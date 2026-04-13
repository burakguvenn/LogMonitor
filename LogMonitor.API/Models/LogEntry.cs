using LogMonitor.API.Models.Enums;

namespace LogMonitor.API.Models;

public class LogEntry
{
    public long Id { get; set; }
    public string Message { get; set; } = null!; // NOT NULL olduğunu belirtiyoruz (C# 8+ nullable kuralı)
    public AppLogLevel Level { get; set; } 
    public DateTime CreatedAt { get; set; } // UTC, datetime2
    public string Source { get; set; } // Soru işareti (?) nullable olduğunu belirtir
}
