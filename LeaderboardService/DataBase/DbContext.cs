using LeaderboardService.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaderboardService.DataAccess;

public class LeaderboardContext : DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<LeaderboardRecord> leaderboardrecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=Kdb222googl_e;Database=irritated_mind_leaderboard;"); 
    }
}