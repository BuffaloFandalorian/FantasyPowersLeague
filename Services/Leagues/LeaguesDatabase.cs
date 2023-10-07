using FantasyPowersLeague.Models;
using MongoDB.Driver;

namespace FantasyPowersLeague.Services
{
    public interface ILeaguesDatabase
    {
        Task<List<LeagueDto>> GetLeaguesAsync();
        Task<LeagueDto> GetLeagueAsync(string id);
        Task<LeagueDto> CreateLeagueAsync(LeagueDto newLeague);
        Task UpdateLeagueAsync(string id, LeagueDto updatedLeague);
        Task RemoveLeagueAsync(string id);
    }

    public class LeaguesDatabase : ILeaguesDatabase
    {
        private IConfiguration _config;
        private IMongoClient _mongoClient;
        private IMongoCollection<LeagueDto> _LeaguesCollection;
        private IMongoDatabase _mongoDatabase;

        public LeaguesDatabase(IConfiguration configuration)
        {
            _config = configuration;
            var connectionString = _config["Mongo:ConnectionString"];
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            _mongoClient = new MongoClient(settings);
            _mongoDatabase = _mongoClient.GetDatabase("FPL");
            _LeaguesCollection = _mongoDatabase.GetCollection<LeagueDto>("Leagues");
        }

        public async Task<List<LeagueDto>> GetLeaguesAsync() => await _LeaguesCollection.Find(_ => true).ToListAsync();
        public async Task<LeagueDto> GetLeagueAsync(string id) => await _LeaguesCollection.Find(x => x.id == id).FirstOrDefaultAsync();
        public async Task<LeagueDto> CreateLeagueAsync(LeagueDto newLeague)
        {
            await _LeaguesCollection.InsertOneAsync(newLeague);
            return newLeague;
        }
        public async Task UpdateLeagueAsync(string id, LeagueDto updatedLeague) => await _LeaguesCollection.ReplaceOneAsync(x => x.id == id, updatedLeague);
        public async Task RemoveLeagueAsync(string id) => await _LeaguesCollection.DeleteOneAsync(x => x.id == id);
    }
}