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
            return new AppIdentity { TokenIsValid = (payload != null), Credential = googleLoginDto.Credential, SessionId = Guid.NewGuid().ToString()};
        }
    }
}