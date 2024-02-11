using LeaderboardService.DataBase.Models;

namespace LeaderboardService.Repositories;

public interface ILeaderboardRepository
{
    Task DeleteAsync(LeaderboardRecord record);
    Task AddAsync(LeaderboardRecord leaderboardRecord);
    Task<IEnumerable<LeaderboardRecord>?> GetTopAsync(int count);
    Task<LeaderboardRecord?> GetByIdAsync(Guid id);
    Task<LeaderboardRecord?> GetByUserIdAsync(Guid id);
    Task<int> GetCountAsync();
    Task<int> GetTopPositionById(Guid id);
}