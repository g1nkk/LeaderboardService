using LeaderboardService.DataBase.Models;

namespace LeaderboardService.Services;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<int> GetUsersCountAsync();
    Task AddUserAsync(User user);

}