using LeaderboardService.DataBase.Models;

namespace LeaderboardService.Services;

public interface ILeaderboardService
{
    Task AddLeaderboardRecordAsync(LeaderboardRecord record);
    Task<IEnumerable<LeaderboardRecord>?> GetTopLeaderboardRecordsAsync(int count);
    Task<LeaderboardRecord?> GetLeaderboardRecordByIdAsync(Guid id);
    Task<int> GetLeaderboardRecordsCountAsync();
}