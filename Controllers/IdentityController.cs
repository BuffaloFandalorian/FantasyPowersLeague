using Microsoft.AspNetCore.Mvc;
using FantasyPowersLeague.Services;
using FantasyPowersLeague.Models;
using Microsoft.AspNetCore.Authorization;

namespace FantasyPowersLeague.Controllers;

[Authorize]
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

    [AllowAnonymous]
    [HttpPost]
    [Route("google-login")]
    [ProducesResponseType(typeof(LoginResultDto), StatusCodes.Status200OK)]
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

    [HttpPost]
    [Route("token-refresh")]
    [ProducesResponseType(typeof(LoginResultDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> TokenRefresh(TokenRefreshDto tokenRefreshDto)
    {
        try
        {
            var refreshResponse = await _identityService.RefreshToken(tokenRefreshDto);
            return Ok(refreshResponse);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            return UnprocessableEntity();
        } 
    }

}
