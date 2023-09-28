using FantasyPowersLeague.Models;

namespace FantasyPowersLeague.Services
{
    public interface IIdentityService
    {
        Task<AppIdentity> LoginWithGoogle(GoogleLoginDto googleLoginDto);
    }
}

