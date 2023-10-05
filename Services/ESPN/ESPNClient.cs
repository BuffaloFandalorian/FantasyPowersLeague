using System.Net.WebSockets;
using FantasyPowersLeague.Controllers;
using FantasyPowersLeague.Models.ESPN;
using Google.Apis.Auth.OAuth2.Requests;

namespace FantasyPowersLeague.Services.ESPN
{
    public interface IESPNClient
    {
        Task<Scoreboard> GetScoreboardAsync(string year);
    }

    public class ESPNClient : IESPNClient
    {
        private HttpClient _httpClient;
        private string _baseUrl;
        public ESPNClient(string baseUrl, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        public async Task<Scoreboard> GetScoreboardAsync(string year)
        {
            try
            {
                var result = await _httpClient.GetAsync($"{_baseUrl}scoreboard?dates={year}");
                if(result.IsSuccessStatusCode)
                {
                    var scoreboard = await result.Content.ReadFromJsonAsync<Scoreboard>();
                    return scoreboard;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}