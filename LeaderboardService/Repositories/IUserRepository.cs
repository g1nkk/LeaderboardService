using LeaderboardService.DataBase.Models;

namespace LeaderboardService.Repositories;

public interface IUserRepository
{
    Task<int> GetAllCountAsync();
    Task<User?> GetByIdAsync(Guid userId);
    Task AddAsync(User user);
}