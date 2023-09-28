using FantasyPowersLeague.Models;

namespace FantasyPowersLeague.Services
{
    public interface IIdentityService
    {
        Task<string> LoginWithGoogle(GoogleLoginDto googleLoginDto);
        string GetJWTToken(AppIdentity appIdentity);
    }
}

