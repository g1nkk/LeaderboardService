using LeaderboardService.DataBase.Models;

namespace LeaderboardService.Repositories;

public interface ILeaderboardRepository
{
    Task AddAsync(LeaderboardRecord leaderboardRecord);
    Task<IEnumerable<LeaderboardRecord>?> GetTopAsync(int count);
    Task<LeaderboardRecord?> GetByIdAsync(Guid id);
    Task<int> GetCountAsync();
}