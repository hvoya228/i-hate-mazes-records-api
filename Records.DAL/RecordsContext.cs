using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Records.Data.Models;

namespace Records.DAL;

public class RecordsContext : DbContext
{
    public RecordsContext(DbContextOptions<RecordsContext> options) : base(options) { }
    
    public DbSet<BestRecord> BestRecords { get; set; }
    public DbSet<Player> Players { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}