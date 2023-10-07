using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Emit;
using FantasyPowersLeague.Helpers;
using FantasyPowersLeague.Models;
using Microsoft.AspNetCore.Routing.Tree;

namespace FantasyPowersLeague.Services
{
    public interface ILeaguesService
    {
        Task<List<LeagueDto>> GetLeaguesAsync();
        Task<LeagueDto> GetLeagueAsync(string id);
        Task<LeagueDto> CreateLeagueAsync(LeagueDto newLeague, HttpRequest request);
        Task UpdateLeagueAsync(string id, LeagueDto updatedLeague);
        Task RemoveLeagueAsync(string id);
        List<DropdownDto> GetLeagueTypes();
    }

    public class LeaguesService : ILeaguesService
    {
        private ILeaguesDatabase _db;
        private IIdentityService _identityService;
        private IUserService _userService;
        public LeaguesService(ILeaguesDatabase leagueDatabase, IIdentityService identityService, IUserService userService)
        {
            _db = leagueDatabase;
            _identityService = identityService;
            _userService = userService;
        }
        public async Task<LeagueDto> GetLeagueAsync(string id) => await _db.GetLeagueAsync(id);
        public async Task<List<LeagueDto>> GetLeaguesAsync() => await _db.GetLeaguesAsync();
        public async Task<LeagueDto> CreateLeagueAsync(LeagueDto newLeague, HttpRequest httpRequest)
        {
            var token = _identityService.ValidateToken(httpRequest);
            var username = token.Claims.Where(x=>x.Type == JwtRegisteredClaimNames.Email).FirstOrDefault()?.Value;
            var userDto = await _userService.GetUserByUsernameAsync(username);
            var members = new LeagueMembers
            {
                 id = userDto.id,
                 isAdmin = true,
                 pendingApproval = false

            };
            newLeague.accessCode = Guid.NewGuid().ToString();
            newLeague.members = new List<LeagueMembers>();
            newLeague.members.Add(members);
            return await _db.CreateLeagueAsync(newLeague);
        } 
        public async Task UpdateLeagueAsync(string id, LeagueDto updatedLeague) => await _db.UpdateLeagueAsync(id, updatedLeague);
        public async Task RemoveLeagueAsync(string id) => await _db.RemoveLeagueAsync(id);
        public List<DropdownDto> GetLeagueTypes()
        {
            List<DropdownDto> dropdownDtos = new List<DropdownDto>();
            foreach(LeagueType value in Enum.GetValues(typeof(LeagueType)))
            {
                dropdownDtos.Add(new DropdownDto{ label = value.GetDescription(), value = (int)value});
            }

            return dropdownDtos;
        }
    }
}