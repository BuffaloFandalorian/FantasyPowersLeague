using FantasyPowersLeague.Models;
using Google.Apis.Auth;

namespace FantasyPowersLeague.Services
{
    public class IdentityService : IIdentityService
    {
        private ILogger<IIdentityService> _logger;
        public IdentityService(ILogger<IdentityService> logger)
        {
            _logger = logger;
        }
        public async Task<AppIdentity> LoginWithGoogle(GoogleLoginDto googleLoginDto)
        {
            //validate token
            GoogleJsonWebSignature.Payload payload =  await GoogleJsonWebSignature.ValidateAsync(googleLoginDto.Credential);
            var appIdentity = new AppIdentity 
                { 
                    TokenIsValid = (payload != null), 
                    Credential = googleLoginDto.Credential, 
                    SessionId = Guid.NewGuid().ToString(),
                    ExpirationDateTime = DateTimeOffset.FromUnixTimeSeconds(payload?.ExpirationTimeSeconds ?? 0).DateTime,
                    IssueDateTime = DateTimeOffset.FromUnixTimeSeconds(payload?.IssuedAtTimeSeconds ?? 0).DateTime
                };
            
            if(appIdentity.ExpirationDateTime >= DateTime.UtcNow)
            {
                throw new Exception("Token is Expired");
            }

            return appIdentity;
        }
    }
}