using MongoDB.Driver;
using MongoDB.Bson;
using FantasyPowersLeague.Models;

namespace FantasyPowersLeague.Services
{
    public interface IRefreshTokenDatabase
    {
        Task<List<RefreshTokenDto>> GetRefreshTokensAsync();
        Task<RefreshTokenDto> GetRefreshTokenByJtiAsync(string Tokenname);
        public Task<RefreshTokenDto> GetRefreshTokenAsync(string id);
        public Task CreateRefreshTokenAsync(RefreshTokenDto newToken);
        public Task UpdateRefreshTokenAsync(string id, RefreshTokenDto updatedToken);
        public Task RemoveRefreshTokenAsync(string id);
        public Task RemoveRefreshTokensAsync(string user);
    }

    public class RefreshTokenDatabase : IRefreshTokenDatabase
    {
        private IConfiguration _config;
        private IMongoClient _mongoClient;
        private IMongoCollection<RefreshTokenDto> _TokensCollection;
        private IMongoDatabase _mongoDatabase;

        public RefreshTokenDatabase(IConfiguration configuration)
        {
            _config = configuration;
            var connectionString = _config["Mongo:ConnectionString"];
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            _mongoClient = new MongoClient(settings);
            _mongoDatabase = _mongoClient.GetDatabase("FPL");
            _TokensCollection = _mongoDatabase.GetCollection<RefreshTokenDto>("RefreshTokens");
        }

        public async Task<List<RefreshTokenDto>> GetRefreshTokensAsync() => await _TokensCollection.Find(_ => true).ToListAsync();
        public async Task<RefreshTokenDto> GetRefreshTokenByJtiAsync(string jti) => await _TokensCollection.Find(x => x.jti == jti).FirstOrDefaultAsync();
        public async Task<RefreshTokenDto> GetRefreshTokenAsync(string id) => await _TokensCollection.Find(x => x.id == id).FirstOrDefaultAsync();
        public async Task CreateRefreshTokenAsync(RefreshTokenDto newToken) => await _TokensCollection.InsertOneAsync(newToken);
        public async Task UpdateRefreshTokenAsync(string id, RefreshTokenDto updatedToken) => await _TokensCollection.ReplaceOneAsync(x => x.id == id, updatedToken);
        public async Task RemoveRefreshTokenAsync(string id) => await _TokensCollection.DeleteOneAsync(x => x.id == id);
        public async Task RemoveRefreshTokensAsync(string user) => await _TokensCollection.DeleteManyAsync(x => x.user == user);

    }
}