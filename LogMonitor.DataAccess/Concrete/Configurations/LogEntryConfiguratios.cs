using Microsoft.EntityFrameworkCore;
using LogMonitor.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogMonitor.DataAccesss.Concrete.Configurations;

public class LogEntryConfiguration : IEntityTypeConfiguration<LogEntry>
{
    public void Configure(EntityTypeBuilder<LogEntry> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Message).IsRequired().HasMaxLength(2000);
        builder.HasIndex(e => new { e.Level, e.CreatedAt });
    }

}