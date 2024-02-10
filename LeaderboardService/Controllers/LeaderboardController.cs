using LeaderboardService.DataBase.Models;
using LeaderboardService.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeaderboardService.Controllers;

[ApiController]
[Route("/leaderboard")]
public class LeaderboardController : ControllerBase
{
    private readonly ILeaderboardService _leaderboardService;

    public LeaderboardController(ILeaderboardService leaderboardService)
    {
        _leaderboardService = leaderboardService;
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetLeaderboardRecordsCount()
    {
        var count = await _leaderboardService.GetLeaderboardRecordsCountAsync();
        return Ok(count);
    }

    [HttpGet]
    public async Task<IActionResult> GetLeaderboardRecordById(Guid id)
    {
        var leaderboardRecord = await _leaderboardService.GetLeaderboardRecordByIdAsync(id);

        if (leaderboardRecord is null)
        {
            return NotFound();
        }

        return Ok(leaderboardRecord);
    }

    [HttpPost]
    public async Task<IActionResult> AddLeaderboardRecord([FromBody] LeaderboardRecord leaderboardRecord)
    {
        await _leaderboardService.AddLeaderboardRecordAsync(leaderboardRecord);
        return Ok();
    }

    [HttpGet("top")]
    public async Task<IActionResult> GetTopList(int count)
    {
        var list = await _leaderboardService.GetTopLeaderboardRecordsAsync(count);

        if (list is null)
        {
            return NotFound();
        }

        return Ok(list);
    }
}