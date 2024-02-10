namespace LeaderboardService.DataBase.Models;

public class LeaderboardRecord
{
    public Guid id { get; set; }
    
    public Guid user_id { get; set; }
    
    public TimeSpan playtime { get; set; }
    public DateTime date { get; set; }
}