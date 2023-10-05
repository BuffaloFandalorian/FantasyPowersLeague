using FantasyPowersLeague.Models.ESPN;

namespace FantasyPowersLeague.Services.ESPN
{
    public interface IESPNService
    {
        Task<List<Scoreboard>> GetScoreboardsAsync();
        Task<Scoreboard> GetScoreboardByYearAsync(string year);
        Task<Scoreboard> GetScoreboardAsync(string id);
        Task CreateScoreboardAsync(Scoreboard newToken);
        Task UpdateScoreboardAsync(string id, Scoreboard updateken);
        Task RemoveScoreboardAsync(string id);
        Task RemoveScoreboardByYearAsync(string year);
        Task<bool> ScoreboardExists(string year);
        Task<string> CacheExpired(string year);
        Task RefreshDatabaseFromServer();
    }
    public class ESPNService : IESPNService
    {
        private ILogger<ESPNService> _logger;
        private IESPNDatabase _ESPNDatabase;

        public ESPNService(ILogger<ESPNService> logger, IESPNDatabase ESPNDatabase)
        {
            _ESPNDatabase = ESPNDatabase;
            _logger = logger;
        }

        public async Task<List<Scoreboard>> GetScoreboardsAsync() => await _ESPNDatabase.GetScoreboardsAsync();
        public async Task<Scoreboard> GetScoreboardByYearAsync(string year) => await _ESPNDatabase.GetScoreboardByYearAsync(year);
        public async Task<Scoreboard> GetScoreboardAsync(string id) => await _ESPNDatabase.GetScoreboardAsync(id);
        public async Task CreateScoreboardAsync(Scoreboard newToken) => await _ESPNDatabase.CreateScoreboardAsync(newToken);
        public async Task UpdateScoreboardAsync(string id, Scoreboard updateken) => await _ESPNDatabase.UpdateScoreboardAsync(id, updateken);
        public async Task RemoveScoreboardAsync(string id) => await _ESPNDatabase.RemoveScoreboardAsync(id);
        public async Task RemoveScoreboardByYearAsync(string year) => await _ESPNDatabase.RemoveScoreboardByYearAsync(year);
        public async Task<bool> ScoreboardExists(string year) => await _ESPNDatabase.ScoreboardExists(year);
        public async Task<string> CacheExpired(string year) => await _ESPNDatabase.CacheExpired(year);
        public async Task RefreshDatabaseFromServer() => await _ESPNDatabase.RefreshDatabaseFromServer();
    }
}