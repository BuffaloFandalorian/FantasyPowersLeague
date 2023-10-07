using FantasyPowersLeague.Models.ESPN;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace FantasyPowersLeague.Services.ESPN
{
    public interface IESPNDatabase
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

    public class ESPNDatabase : IESPNDatabase
    {
        private ILogger<ESPNDatabase> _logger;
        private IConfiguration _config;
        private IMongoClient _mongoClient;
        private IMongoCollection<Scoreboard> _scoreboardCollection;
        private IMongoDatabase _mongoDatabase;
        private IESPNClient _espnClient;
        private string _activeYear;
        private string [] _archiveYears;
        private int _cacheTimeMinutes;
        

        public ESPNDatabase(ILogger<ESPNDatabase> logger, IConfiguration config, IESPNClient ESPNClient)
        {
            _logger = logger;
            _config = config;
            var connectionString = _config["Mongo:ConnectionString"];
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            _mongoClient = new MongoClient(settings);
            _mongoDatabase = _mongoClient.GetDatabase("FPL");
            _scoreboardCollection = _mongoDatabase.GetCollection<Scoreboard>("Scoreboard");
            _espnClient = ESPNClient;
            _activeYear = config["ESPN:ActiveYear"];
            _archiveYears = config["ESPN:CacheYears"].Split(',');
            _cacheTimeMinutes = int.Parse(config["ESPN:CacheTimeMinutes"]);
        }

        public async Task RefreshDatabaseFromServer()
        {
            //get/set archive
            foreach(var year in _archiveYears)
            {
                if(!await ScoreboardExists(year))
                {
                    var scoreboard = await _espnClient.GetScoreboardAsync(year);
                    if(scoreboard != null)
                    {
                        scoreboard.year = year;
                        scoreboard.isArchived = true;
                        scoreboard.lastSync = DateTime.UtcNow;
                        await CreateScoreboardAsync(scoreboard);
                    }
                }
            }

            //get/set active
            if(!await ScoreboardExists(_activeYear))
            {
                var scoreboard = await _espnClient.GetScoreboardAsync(_activeYear);
                if(scoreboard != null)
                {
                    scoreboard.year = _activeYear;
                    scoreboard.isArchived = false;
                    scoreboard.lastSync = DateTime.UtcNow;
                    await CreateScoreboardAsync(scoreboard);
                }
            }
            else
            {
                var id = await CacheExpired(_activeYear);
                //check cache time
                if(!String.IsNullOrWhiteSpace(id))
                {
                    var scoreboard = await _espnClient.GetScoreboardAsync(_activeYear);
                    if(scoreboard != null)
                    {
                        scoreboard.year = _activeYear;
                        scoreboard.isArchived = false;
                        scoreboard.lastSync = DateTime.UtcNow;
                        scoreboard.id = id;
                        await UpdateScoreboardAsync(id, scoreboard);
                    }
                }
            }
        }

        public async Task<List<Scoreboard>> GetScoreboardsAsync() => await _scoreboardCollection.Find(_ => true).ToListAsync();
        public async Task<Scoreboard> GetScoreboardByYearAsync(string year) => await _scoreboardCollection.Find(x => x.year == year).FirstOrDefaultAsync();
        public async Task<Scoreboard> GetScoreboardAsync(string id) => await _scoreboardCollection.Find(x => x.id == id).FirstOrDefaultAsync();
        public async Task CreateScoreboardAsync(Scoreboard newToken) => await _scoreboardCollection.InsertOneAsync(newToken);
        public async Task UpdateScoreboardAsync(string id, Scoreboard updateken) => await _scoreboardCollection.ReplaceOneAsync(x => x.id == id, updateken);
        public async Task RemoveScoreboardAsync(string id) => await _scoreboardCollection.DeleteOneAsync(x => x.id == id);
        public async Task RemoveScoreboardByYearAsync(string year) => await _scoreboardCollection.DeleteOneAsync(x => x.year == year);
        public async Task<bool> ScoreboardExists(string year) => await _scoreboardCollection.Find(x => x.year == year).AnyAsync();
        public async Task<string> CacheExpired(string year)
        {
            var expiry = DateTime.UtcNow.AddMinutes(-1 *_cacheTimeMinutes);
            return (await _scoreboardCollection.Find(x => x.year == year && x.lastSync < expiry).FirstOrDefaultAsync())?.id;
        }
        
    }


}