using System.Text.Json;
using LeaderboardService.DataAccess;
using LeaderboardService.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace LeaderboardService.Repositories;

public class LeaderboardRepository : ILeaderboardRepository
{
    private readonly LeaderboardContext _dbContext;
    private readonly IDatabase _redisDb;
    
    public LeaderboardRepository(LeaderboardContext dbContext, IDatabase redisDb)
    {
        _dbContext = dbContext;
        _redisDb = redisDb;
    }

    public async Task AddAsync(LeaderboardRecord leaderboardRecord)
    {
        await _dbContext.AddAsync(leaderboardRecord);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(LeaderboardRecord leaderboardRecord)
    {
        _dbContext.leaderboardrecords.Remove(leaderboardRecord);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<LeaderboardRecord>?> GetTopAsync(int count)
    {
        var cachedData = await _redisDb.StringGetAsync("leaderboard_top");

        if (!cachedData.IsNullOrEmpty)
        {
            var cachedRecords = JsonSerializer.Deserialize<IEnumerable<LeaderboardRecord>>(cachedData);
            return cachedRecords.Take(count);
        }

        IEnumerable<LeaderboardRecord> list = await _dbContext.leaderboardrecords
            .OrderByDescending(lr => lr.playtime)
            .Take(count)
            .Include(lr => lr.User)
            .ToListAsync();
        
        UpdateCacheAsync();

        return list;
    }

    public async Task UpdateCacheAsync()
    {
        var recordsFromDb = await _dbContext.leaderboardrecords
            .OrderByDescending(lr => lr.playtime)
            .Take(100)
            .Include(lr => lr.User)
            .ToListAsync();
        
        await _redisDb.StringSetAsync("leaderboard_top", JsonSerializer.Serialize(recordsFromDb));
    }

    public async Task<LeaderboardRecord?> GetByIdAsync(Guid id)
    {
        var leaderboardRecord = await _dbContext.leaderboardrecords
            .SingleOrDefaultAsync(lr => lr.id == id);

        return leaderboardRecord;
    }

    public async Task<LeaderboardRecord?> GetByUserIdAsync(Guid id)
    {
        var leaderboardRecord = await _dbContext.leaderboardrecords
            .SingleOrDefaultAsync(lr => lr.user_id == id);

        return leaderboardRecord;
    }

    public async Task<int> GetCountAsync()
    {
        return await _dbContext.leaderboardrecords.CountAsync();
    }

    public async Task<int> GetTopPositionById(Guid id)
    {
        List<LeaderboardRecord> list = await _dbContext.leaderboardrecords
            .OrderByDescending(lr => lr.playtime)
            .ToListAsync();

        var record = list.Single(lr => lr.id == id);
        return list.IndexOf(record) + 1;
    }
}