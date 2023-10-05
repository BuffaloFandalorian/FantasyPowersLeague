using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyPowersLeague.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Games")]
    public class GamesController : ControllerBase
    {
        private readonly ILogger<GamesController> _logger;
        
        public GamesController(ILogger<GamesController> logger)
        {
            _logger = logger;
        }
    }
}