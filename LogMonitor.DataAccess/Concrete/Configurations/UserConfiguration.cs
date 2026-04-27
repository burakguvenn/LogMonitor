using Microsoft.EntityFrameworkCore;
using LogMonitor.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LogMonitor.DataAccesss.Concrete.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Username).IsRequired().HasMaxLength(40);
        builder.Property(x => x.ApiKey).IsRequired().HasMaxLength(100);
        builder.HasIndex(x => x.ApiKey).IsUnique();

        builder.HasMany(u => u.LogEntries) // 1 user'ın çok sayıda logu olabilir
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade); // User silinirse logları da silinir

    }
}