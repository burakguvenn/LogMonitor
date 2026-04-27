using Microsoft.EntityFrameworkCore;
using LogMonitor.Entities.Models;

namespace LogMonitor.DataAccess.Concrete.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<LogEntry> LogEntries => Set<LogEntry>();
    public DbSet<User> Users => Set<User>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
