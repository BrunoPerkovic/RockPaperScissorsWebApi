using Microsoft.AspNetCore.Mvc;
using RockPaperScissorsAPI.BL.Interfaces;
using RockPaperScissorsAPI.BL.Models;
using RockPaperScissorsAPI.BL.Utils;

namespace RockPaperScissorsAPI.Controllers;

[Route("api/[controller]/[action]")]
public class RpsController : ControllerBase
{
    private readonly IRpsService _rpsService;
    private readonly ILogger<RpsController> _logger;

    public RpsController(IRpsService rpsService, ILogger<RpsController> logger)
    {
        _rpsService = rpsService;
        _logger = logger;
    }

    /*
    [HttpPost]
    public IActionResult Rock()
    {
        _logger.LogInformation("User played rock before calling service");
        _rpsService.Rock();
        _logger.LogInformation("User played rock after calling service");
        return Ok();
    }

    [HttpPost]
    public IActionResult Paper()
    {
        _logger.LogInformation("User played paper before calling service");
        _rpsService.Paper();
        _logger.LogInformation("User played paper after calling service");
        return Ok();
    }

    [HttpPost]
    public IActionResult Scissors()
    {
        _logger.LogInformation("User played scissors before calling service");
        _rpsService.Scissors();
        _logger.LogInformation("User played scissors after calling service");
        return Ok();
    }
    */

    [HttpGet("{id}")]
    public IActionResult Stats(int id)
    {
        _logger.LogInformation("Getting player stats");
        _rpsService.GetPlayerStats(id);
        _logger.LogInformation("Got player stats");
        return Ok();
    }

    [HttpGet]
    public IActionResult AllStats()
    {
        _logger.LogInformation("Getting all player stats");
        _rpsService.GetAllPlayerStats();
        _logger.LogInformation("Got all player stats");
        return Ok();
    }

    [HttpPost]
    public IActionResult PlayGame([FromBody] PlayGameRequest request)
    {
        if (request == null)
        {
            return BadRequest("Invalid request");
        }

        if (request.PlayerAction == RpsAction.Unknown)
        {
            return BadRequest("Invalid action");
        }

        _rpsService.PlayGame(request.PlayerId, request.PlayerAction);
        return Ok();
    }
    
    [HttpPost]
    public IActionResult ResetStats(int playerId)
    {
        _rpsService.ResetStats(playerId);
        return Ok();
    }
}