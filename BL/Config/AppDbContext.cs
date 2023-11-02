using RockPaperScissorsAPI.BL.Models;

namespace RockPaperScissorsAPI.BL.Config;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<GameResult> GameResults { get; set; }
}
