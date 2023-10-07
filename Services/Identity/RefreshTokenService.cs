using System.Data.Common;
using FantasyPowersLeague.Models;

namespace FantasyPowersLeague.Services
{
    public interface IRefreshTokenService
    {
        Task<List<RefreshTokenDto>> GetRefreshTokensAsync();
        Task<RefreshTokenDto> GetRefreshTokenByJtiAsync(string jti);
        Task<RefreshTokenDto> GetRefreshTokenAsync(string id);
        Task CreateRefreshTokenAsync(RefreshTokenDto newRefreshToken);
        Task UpdateRefreshTokenAsync(string id, RefreshTokenDto updatedRefreshToken);
        Task RemoveRefreshTokenAsync(string id);
        Task RemoveRefreshTokensAsync(string user);
    }

    public class RefreshTokenService : IRefreshTokenService
    {
        private IRefreshTokenDatabase _db;
        public RefreshTokenService(IRefreshTokenDatabase RefreshTokenDatabase)
        {
            _db = RefreshTokenDatabase;
        }

        public async Task<RefreshTokenDto> GetRefreshTokenAsync(string id) => await _db.GetRefreshTokenAsync(id);
        public async Task<RefreshTokenDto> GetRefreshTokenByJtiAsync(string jti) => await _db.GetRefreshTokenByJtiAsync(jti);
        public async Task<List<RefreshTokenDto>> GetRefreshTokensAsync() => await _db.GetRefreshTokensAsync();
        public async Task CreateRefreshTokenAsync(RefreshTokenDto newRefreshToken) => await _db.CreateRefreshTokenAsync(newRefreshToken);
        public async Task UpdateRefreshTokenAsync(string id, RefreshTokenDto updatedRefreshToken) => await _db.UpdateRefreshTokenAsync(id, updatedRefreshToken);
        public async Task RemoveRefreshTokenAsync(string id) => await _db.RemoveRefreshTokenAsync(id);
        public async Task RemoveRefreshTokensAsync(string user) => await _db.RemoveRefreshTokensAsync(user);
    }
}