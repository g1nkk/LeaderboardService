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
    
    public async Task AddLeaderboardRecordAsync(LeaderboardRecord record)
    {
        await _leaderboardRepository.AddAsync(record);
    }

    public async Task<IEnumerable<LeaderboardRecord>?> GetTopLeaderboardRecordsAsync(int count)
    {
        return await _leaderboardRepository.GetTopAsync(count);
    }

    public async Task<LeaderboardRecord?> GetLeaderboardRecordByIdAsync(Guid id)
    {
        return await _leaderboardRepository.GetByIdAsync(id);
    }

    public async Task<int> GetLeaderboardRecordsCountAsync()
    {
        return await _leaderboardRepository.GetCountAsync();
    }
}