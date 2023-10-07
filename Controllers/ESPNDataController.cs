using FantasyPowersLeague.Models.ESPN;
using FantasyPowersLeague.Services.ESPN;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyPowersLeague.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/espn")]
    public class EspnController : ControllerBase
    {
        private readonly ILogger<EspnController> _logger;
        private readonly IESPNService _espnService;
        
        public EspnController(ILogger<EspnController> logger, IESPNService eSPNService)
        {
            _logger = logger;
            _espnService = eSPNService;
        }

        [HttpGet]
        [Route("scoreboards")]
        [ProducesResponseType(typeof(List<Scoreboard>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetScoreboardsAsync()
        {
            try
            {
                var result = await _espnService.GetScoreboardsAsync();
                if(result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }
            catch
            {
                return UnprocessableEntity();
            }
        }
        
        [Authorize(Policy = "Admin")]
        [HttpGet]
        [Route("scoreboards/refresh")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RefreshDatabaseFromServer()
        {
            try
            {
                await _espnService.RefreshDatabaseFromServer();
                return NoContent();
            }
            catch
            {
                return UnprocessableEntity();
            }
        }
    }
}