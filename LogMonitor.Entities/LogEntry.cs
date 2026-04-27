using LogMonitor.Entities.Models.Enums;

namespace LogMonitor.Entities.Models;

public class LogEntry
{
    public long Id { get; set; }
    public string Message { get; set; } = null!; // NOT NULL olduğunu belirtiyoruz (C# 8+ nullable kuralı)
    public AppLogLevel Level { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(3);
    public int UserId { get; set; } // FK
    public User User { get; set; } = null!;
    public string? Metadata { get; set; }
}
