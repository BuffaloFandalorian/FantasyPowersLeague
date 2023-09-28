using FantasyPowersLeague.Models;

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
            return new AppIdentity { Credential = googleLoginDto.Credential, SessionId = Guid.NewGuid().ToString()};
        }
    }
}