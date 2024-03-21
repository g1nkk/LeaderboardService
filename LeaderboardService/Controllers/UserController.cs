using LeaderboardService.DataBase.Models;
using LeaderboardService.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LeaderboardService.Controllers;

[ApiController]
[Route("/users")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetUserCount()
    {
        var count = await _userService.GetUsersCountAsync();

        return Ok(count);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] User user)
    {
        if (await _userService.GetUserByIdAsync(user.id) is not null)
        {
            return Conflict();
        }
        
        await _userService.AddUserAsync(user);

        _logger.LogInformation($"NEW USER WAS CREATED: {user.name}");
        
        return Ok();
    }
}