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
        Task<LoginResultDto> RefreshToken(TokenRefreshDto tokenRefreshDto);
    }
    public class IdentityService : IIdentityService
    {
        private ILogger<IIdentityService> _logger;
        private IConfiguration _config;
        private IUserService _userService;

        public IdentityService(ILogger<IdentityService> logger, IConfiguration configuration, IUserService userService)
        {
            _logger = logger;
            _config = configuration;
            _userService = userService;
        }

        #region internal methods
        protected string GetJWTToken(AppIdentity appIdentity)
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, appIdentity.email),
                    new Claim(JwtRegisteredClaimNames.Email, appIdentity.email),
                    new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
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
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);
            
            return stringToken;
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

                var token = GetJWTToken(appIdentity);

                if(String.IsNullOrWhiteSpace(token))
                {
                    throw new Exception("Token generated is null or whitespace.");
                }

                var userId = await GetOrRegisterUser(payload.Email);

                if(userId == null)
                {
                    throw new Exception("Error registering or fetching user");
                }

                var loginResponse = new LoginResultDto
                {
                     token = token,
                     expiration = payload.ExpirationTimeSeconds,
                     userId = userId
                };

                return loginResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error Generating JWT: {ex.Message}");
                throw ex;
            }
        }

        public async Task<LoginResultDto> RefreshToken(TokenRefreshDto tokenRefreshDto)
        {
            return new LoginResultDto();
        }
    }
}