using LeaderboardService.DataAccess;
using LeaderboardService.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaderboardService.Repositories;

public class LeaderboardRepository : ILeaderboardRepository
{
    private readonly LeaderboardContext _dbContext;

    public LeaderboardRepository(LeaderboardContext dbContext)
    {
        _dbContext = dbContext;
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
        IEnumerable<LeaderboardRecord> list = await _dbContext.leaderboardrecords
            .OrderByDescending(lr => lr.playtime)
            .Take(count)
            .ToListAsync();

        return list;
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