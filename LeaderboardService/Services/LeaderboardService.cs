using LeaderboardService.DataBase.Models;
using LeaderboardService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LeaderboardService.Services;

public class LeaderboardService : ILeaderboardService
{
    private readonly ILeaderboardRepository _leaderboardRepository;

    public LeaderboardService(ILeaderboardRepository leaderboardRepository)
    {
        _leaderboardRepository = leaderboardRepository;
    }

    public async Task AddOrUpdateUserRecordAsync(LeaderboardRecord record)
    {
        var previousRecord = await _leaderboardRepository.GetByUserIdAsync(record.user_id);

        if (previousRecord is not null)
        {
            await _leaderboardRepository.DeleteAsync(previousRecord);
        }

        var recordWithoutUser = new LeaderboardRecord
        {
            date = record.date,
            id = record.id,
            playtime = record.playtime,
            user_id = record.user_id
        };
        await _leaderboardRepository.AddAsync(recordWithoutUser);
        await _leaderboardRepository.UpdateCacheAsync();
    }

    public async Task<IEnumerable<LeaderboardRecord>?> GetTopLeaderboardRecordsAsync(int count)
    {
        return await _leaderboardRepository.GetTopAsync(count);
    }

    public async Task<LeaderboardRecord?> GetLeaderboardRecordByIdAsync(Guid id)
    {
        return await _leaderboardRepository.GetByIdAsync(id);
    }

    public async Task<LeaderboardRecord?> GetLeaderboardRecordByUserIdAsync(Guid id)
    {
        return await _leaderboardRepository.GetByUserIdAsync(id);
    }

    public async Task<int> GetLeaderboardRecordsCountAsync()
    {
        return await _leaderboardRepository.GetCountAsync();
    }

    public async Task<int> GetLeaderboardRecordTopPositionAsync(Guid id)
    {
        return await _leaderboardRepository.GetTopPositionById(id);
    }
}