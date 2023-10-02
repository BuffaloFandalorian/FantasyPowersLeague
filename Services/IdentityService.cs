using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FantasyPowersLeague.Models;
using Google.Apis.Auth;
using Microsoft.IdentityModel.Tokens;

namespace FantasyPowersLeague.Services
{
    public interface IIdentityService
    {
        Task<LoginResultDto> LoginWithGoogle(GoogleLoginDto googleLoginDto);
        Task<LoginResultDto> RefreshToken(TokenDto tokenDto);
    }
    public class IdentityService : IIdentityService
    {
        private ILogger<IIdentityService> _logger;
        private IConfiguration _config;
        private IUserService _userService;
        private IRefreshTokenService _refreshTokenService;
        //private GoogleTokenVerifier _googleTokenVerifier;

        public IdentityService(
            ILogger<IdentityService> logger, 
            IConfiguration configuration, 
            IUserService userService,
            IRefreshTokenService refreshTokenService)
        {
            _logger = logger;
            _config = configuration;
            _userService = userService;
            _refreshTokenService = refreshTokenService;
        }

        #region internal methods
        protected GeneratedTokenDto GetJWTToken(AppIdentity appIdentity, bool isRefresh = false)
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var jti = Guid.NewGuid();
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, appIdentity.email),
                    new Claim(JwtRegisteredClaimNames.Email, appIdentity.email),
                    new Claim(JwtRegisteredClaimNames.Jti, jti.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),

                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            
            return new GeneratedTokenDto
            {
                 isRefresh = isRefresh,
                 token = stringToken,
                 jti = jti,
                 expiration = (long)(tokenDescriptor.Expires - new DateTime(1970, 1, 1)).Value.TotalSeconds
            };
        }

        private async Task<string> GetOrRegisterUser(string username)
        {
            try
            {
                var user = await _userService.GetUserByUsernameAsync(username);
                if(user == null)
                {
                    var newUser = new UserDto
                    {
                        username = username
                    };
                    await _userService.CreateUserAsync(newUser);
                    user = await _userService.GetUserByUsernameAsync(username);
                }
                return user.id;
            }
            catch(Exception ex)
            {
                _logger.LogError($"GetOrRegisterUser: {ex.Message}");
                throw ex;
            }

        }
        #endregion

        public async Task<LoginResultDto> LoginWithGoogle(GoogleLoginDto googleLoginDto)
        {
            try
            {
                //validate token
                GoogleJsonWebSignature.Payload payload =  await GoogleJsonWebSignature.ValidateAsync(googleLoginDto.Credential);

                if(payload == null)
                {
                    throw new Exception("Payload from Google is null");
                }

                var expiration = DateTimeOffset.FromUnixTimeSeconds(payload?.ExpirationTimeSeconds ?? 0).DateTime;
                
                if(expiration <= DateTime.UtcNow)
                {
                    throw new Exception("Token is Expired");
                }

                if(String.IsNullOrWhiteSpace(payload?.Email))
                {
                    throw new Exception("No Email Address in Google Token");
                }

                var appIdentity = new AppIdentity 
                {
                    email = payload.Email
                };

                var generatedToken = GetJWTToken(appIdentity);

                if(String.IsNullOrWhiteSpace(generatedToken.token))
                {
                    throw new Exception("Token generated is null or whitespace.");
                }

                var refreshToken = GetJWTToken(appIdentity, true);

                if(String.IsNullOrWhiteSpace(refreshToken.token))
                {
                    throw new Exception("Refresh Token generated is null or whitespace.");
                }
                else
                {
                    //expire old refresh tokens
                    await _refreshTokenService.RemoveRefreshTokensAsync(payload.Email);
                    //store for refresh whitelisting
                    await _refreshTokenService.CreateRefreshTokenAsync(new RefreshTokenDto { jti = refreshToken.jti.ToString() });
                }

                var userId = await GetOrRegisterUser(payload.Email);

                if(userId == null)
                {
                    throw new Exception("Error registering or fetching user");
                }

                //generate refresh token
                

                var loginResponse = new LoginResultDto
                {
                     token = generatedToken.token,
                     expiration = payload.ExpirationTimeSeconds,
                     userId = userId,
                     refreshToken = refreshToken.token
                };

                return loginResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error Generating JWT: {ex.Message}");
                throw ex;
            }
        }

        public async Task<LoginResultDto> RefreshToken(TokenDto tokenRefreshDto)
        {
            try
            {
                var issuer = _config["Jwt:Issuer"];
                var audience = _config["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = issuer,
                    ValidAudience = audience
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var claminsPrincipal = tokenHandler.ValidateToken(tokenRefreshDto.token, validationParameters, out SecurityToken securityToken);
                var jwtToken = (JwtSecurityToken)securityToken;
                var jti = jwtToken.Claims.FirstOrDefault(x=>x.Type == JwtRegisteredClaimNames.Jti);

                //we use token whitelisting.  First see if this token is whitelisted
                if(jti == null)
                {
                    _logger.LogError($"jti not found in token payload {tokenRefreshDto.token.ToString()}");
                    return null;
                }

                var jtiRecord = await _refreshTokenService.GetRefreshTokenByJtiAsync(jti.Value);
                
                if(jtiRecord != null)
                {
                    await _refreshTokenService.RemoveRefreshTokensAsync(jtiRecord.user); //expire all the existing tokens for this user
                    var newRefreshToken = GetJWTToken(new AppIdentity { email = jtiRecord.user }, true);
                    await _refreshTokenService.CreateRefreshTokenAsync(new RefreshTokenDto{ jti = newRefreshToken.jti.ToString(), user = jtiRecord.user});
                    var newToken = GetJWTToken(new AppIdentity { email = jtiRecord.user }, false);
                    return new LoginResultDto { refreshToken = newRefreshToken.token, token = newToken.token, userId = jtiRecord.user, expiration = newToken.expiration };
                }
                else
                {
                    _logger.LogError($"jti not found in whitelist {tokenRefreshDto.token.ToString()}");
                    return null;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error refreshing token: {ex.Message}");
                return null;
            }
        }
    }
}