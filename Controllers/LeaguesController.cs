using FantasyPowersLeague.Models;
using FantasyPowersLeague.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyPowersLeague.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/leagues")]
    public class LeaguesController : ControllerBase
    {
        private readonly ILogger<LeaguesController> _logger;
        private readonly ILeaguesService _leaguesService;

        public LeaguesController(ILogger<LeaguesController> logger, ILeaguesService leaguesService)
        {
            _logger = logger;
            _leaguesService = leaguesService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("types")]
        [ProducesResponseType(typeof(List<DropdownDto>), StatusCodes.Status200OK)]
        public IActionResult GetLeagueTypes()
        {
            try
            {
                var result = _leaguesService.GetLeagueTypes();
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

        [HttpPost]
        [Route("pick-em")]
        [ProducesResponseType(typeof(LeagueDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> PickEm(LeagueDto leagueDto)
        {
            try
            {
                var result = await _leaguesService.CreateLeagueAsync(leagueDto);
                return Ok(result);
            }
            catch
            {
                return UnprocessableEntity();
            }
        }
    }
}