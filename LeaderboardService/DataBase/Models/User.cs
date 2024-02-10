using System.ComponentModel.DataAnnotations;

namespace LeaderboardService.DataBase.Models;

public class User
{
    public Guid id { get; set; }
    
    [Required]
    [StringLength(20)]
    public string name { get; set; }
}