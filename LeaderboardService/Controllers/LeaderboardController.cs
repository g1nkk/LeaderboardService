using System.ComponentModel.DataAnnotations;
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
    public async Task<IActionResult> GetLeaderboardRecordByUserId(Guid id)
    {
        var leaderboardRecord = await _leaderboardService.GetLeaderboardRecordByUserIdAsync(id);

        if (leaderboardRecord is null)
        {
            return NotFound();
        }

        return Ok(leaderboardRecord);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrUpdateLeaderboardRecord([FromBody] LeaderboardRecord leaderboardRecord)
    {
        await _leaderboardService.AddOrUpdateUserRecordAsync(leaderboardRecord);
        return Ok();
    }

    [HttpGet("top")]
    public async Task<IActionResult> GetLeaderboardTopList([FromQuery][Range(0, 100)]int count)
    {
        var list = await _leaderboardService.GetTopLeaderboardRecordsAsync(count);  

        if (list is null)
        {
            return NotFound();
        }

        return Ok(list);
    }
    
    [HttpGet("top/position")]
    public async Task<IActionResult> GetLeaderboardTopPositionById(Guid id)
    {
        var record = await _leaderboardService.GetLeaderboardRecordByIdAsync(id);

        if (record is null)
        {
            return NotFound();
        }

        int position = await _leaderboardService.GetLeaderboardRecordTopPositionAsync(id);

        return Ok(position);
    }
}