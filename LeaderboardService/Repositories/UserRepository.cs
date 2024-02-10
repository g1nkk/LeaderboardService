using LeaderboardService.DataAccess;
using LeaderboardService.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaderboardService.Repositories;

public class UserRepository : IUserRepository
{
    private readonly LeaderboardContext _dbContext;

    public UserRepository(LeaderboardContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<int> GetAllCountAsync()
    {
        return await _dbContext.users.CountAsync();
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        var user = await _dbContext.users.SingleOrDefaultAsync(user => user.id == userId);
        return user;
    }

    public async Task<User?> GetByNameAsync(string name)
    {
        var user = await _dbContext.users.SingleOrDefaultAsync(user => user.name == name);
        return user;
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }
}