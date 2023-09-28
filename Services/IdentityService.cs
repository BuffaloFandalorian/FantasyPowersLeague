using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FantasyPowersLeague.Models;
using Google.Apis.Auth;
using Microsoft.IdentityModel.Tokens;

namespace FantasyPowersLeague.Services
{
    public class IdentityService : IIdentityService
    {
        private ILogger<IIdentityService> _logger;
        private IConfiguration _config;

        public IdentityService(ILogger<IdentityService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
        }

        public string GetJWTToken(AppIdentity appIdentity)
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

        public async Task<string> LoginWithGoogle(GoogleLoginDto googleLoginDto)
        {
            try
            {
                //validate token
                GoogleJsonWebSignature.Payload payload =  await GoogleJsonWebSignature.ValidateAsync(googleLoginDto.Credential);

                if(payload == null)
                {
                    throw new Exception("Payload from Google is null");
                }
                
                if(DateTimeOffset.FromUnixTimeSeconds(payload?.ExpirationTimeSeconds ?? 0).DateTime <= DateTime.UtcNow)
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

                return token;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error Generating JWT: {ex.Message}");
                throw ex;
            }
        }
    }
}