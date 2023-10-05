using MongoDB.Driver;
using MongoDB.Bson;
using FantasyPowersLeague.Models;

namespace FantasyPowersLeague.Services
{
    public interface IUserDatabase
    {
        Task<List<UserDto>> GetUsersAsync();
        Task<UserDto> GetUserByUsernameAsync(string username);
        public Task<UserDto> GetUserAsync(string id);
        public Task CreateUserAsync(UserDto newuser);
        public Task UpdateUserAsync(string id, UserDto updateduser);
        public Task RemoveUserAsync(string id);
    }

    public class UserDatabase : IUserDatabase
    {
        private IConfiguration _config;
        private IMongoClient _mongoClient;
        private IMongoCollection<UserDto> _usersCollection;
        private IMongoDatabase _mongoDatabase;

        public UserDatabase(IConfiguration configuration)
        {
            _config = configuration;
            var connectionString = _config["Mongo:ConnectionString"];
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            _mongoClient = new MongoClient(settings);
            _mongoDatabase = _mongoClient.GetDatabase("FPL");
            _usersCollection = _mongoDatabase.GetCollection<UserDto>("Users");
        }

        public async Task<List<UserDto>> GetUsersAsync() => await _usersCollection.Find(_ => true).ToListAsync();
        public async Task<UserDto> GetUserByUsernameAsync(string username) => await _usersCollection.Find(x => x.username == username).FirstOrDefaultAsync();
        public async Task<UserDto> GetUserAsync(string id) => await _usersCollection.Find(x => x.id == id).FirstOrDefaultAsync();
        public async Task CreateUserAsync(UserDto newuser) => await _usersCollection.InsertOneAsync(newuser);
        public async Task UpdateUserAsync(string id, UserDto updateduser) => await _usersCollection.ReplaceOneAsync(x => x.id == id, updateduser);
        public async Task RemoveUserAsync(string id) => await _usersCollection.DeleteOneAsync(x => x.id == id);

    }
}