using System.Data.Common;
using FantasyPowersLeague.Models;

namespace FantasyPowersLeague.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsersAsync();
        Task<UserDto> GetUserByUsernameAsync(string username);
        Task<UserDto> GetUserAsync(string id);
        Task CreateUserAsync(UserDto newuser);
        Task UpdateUserAsync(string id, UserDto updateduser);
        Task RemoveUserAsync(string id);
    }

    public class UserService : IUserService
    {
        private IUserDatabase _db;
        public UserService(IUserDatabase userDatabase)
        {
            _db = userDatabase;
        }

        public async Task<UserDto> GetUserAsync(string id) => await _db.GetUserAsync(id);
        public async Task<UserDto> GetUserByUsernameAsync(string username) => await _db.GetUserByUsernameAsync(username);
        public async Task<List<UserDto>> GetUsersAsync() => await _db.GetUsersAsync();
        public async Task CreateUserAsync(UserDto newuser) => await _db.CreateUserAsync(newuser);
        public async Task UpdateUserAsync(string id, UserDto updateduser) => await _db.UpdateUserAsync(id, updateduser);
        public async Task RemoveUserAsync(string id) => await _db.RemoveUserAsync(id);
    }
}