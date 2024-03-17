using LeaderboardService.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaderboardService.DataAccess;

public class LeaderboardContext : DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<LeaderboardRecord> leaderboardrecords { get; set; }
    
    public LeaderboardContext(DbContextOptions<LeaderboardContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LeaderboardRecord>()
            .HasOne(lr => lr.User) 
            .WithMany()
            .HasForeignKey(lr => lr.user_id);
    }
}