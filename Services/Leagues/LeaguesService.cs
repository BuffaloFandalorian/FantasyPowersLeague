using System.Reflection.Emit;
using FantasyPowersLeague.Helpers;
using FantasyPowersLeague.Models;

namespace FantasyPowersLeague.Services
{
    public interface ILeaguesService
    {
        Task<List<LeagueDto>> GetLeaguesAsync();
        Task<LeagueDto> GetLeagueAsync(string id);
        Task<LeagueDto> CreateLeagueAsync(LeagueDto newLeague);
        Task UpdateLeagueAsync(string id, LeagueDto updatedLeague);
        Task RemoveLeagueAsync(string id);
        List<DropdownDto> GetLeagueTypes();
    }

    public class LeaguesService : ILeaguesService
    {
        private ILeaguesDatabase _db;
        public LeaguesService(ILeaguesDatabase leagueDatabase)
        {
            _db = leagueDatabase;
        }
        public async Task<LeagueDto> GetLeagueAsync(string id) => await _db.GetLeagueAsync(id);
        public async Task<List<LeagueDto>> GetLeaguesAsync() => await _db.GetLeaguesAsync();
        public async Task<LeagueDto> CreateLeagueAsync(LeagueDto newLeague) => await _db.CreateLeagueAsync(newLeague);
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