using Microsoft.AspNetCore.Mvc;
using FantasyPowersLeague.Services;
using FantasyPowersLeague.Models;

namespace FantasyPowersLeague.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController : ControllerBase
{
    private readonly ILogger<IdentityController> _logger;
    private readonly IIdentityService _identityService;

    public IdentityController(ILogger<IdentityController> logger, IIdentityService identityService)
    {
        _logger = logger;
        _identityService = identityService;
    }

    [HttpGet]
    [Route("healthcheck")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> Healthcheck()
    {
        return Ok("Hello!");
    }

    [HttpPost]
    [Route("google-login")]
    [ProducesResponseType(typeof(AppIdentity), StatusCodes.Status200OK)]
    public async Task<IActionResult> GoogleLogin(GoogleLoginDto googleLoginDto)
    {
        try
        {
            var loginResponse = await _identityService.LoginWithGoogle(googleLoginDto);
            return Ok(loginResponse);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return UnprocessableEntity();
        }
    }
}
