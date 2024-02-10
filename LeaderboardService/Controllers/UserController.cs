using LeaderboardService.DataBase.Models;
using LeaderboardService.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LeaderboardService.Controllers;

[ApiController]
[Route("/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
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
        if (await _userService.GetUserByNameAsync(user.name) is not null)
        {
            return Conflict();
        }
        
        await _userService.AddUserAsync(user);
        return Ok();
    }
}