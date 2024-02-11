using LeaderboardService.DataBase.Models;

namespace LeaderboardService.Services;

public interface ILeaderboardService
{
    Task<IEnumerable<LeaderboardRecord>?> GetTopLeaderboardRecordsAsync(int count);
    Task<LeaderboardRecord?> GetLeaderboardRecordByIdAsync(Guid id);
    Task<LeaderboardRecord?> GetLeaderboardRecordByUserIdAsync(Guid id);
    Task<int> GetLeaderboardRecordsCountAsync();
    Task<int> GetLeaderboardRecordTopPositionAsync(Guid id);
    Task AddOrUpdateUserRecordAsync(LeaderboardRecord record);
}