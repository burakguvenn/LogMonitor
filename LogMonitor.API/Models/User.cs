namespace LogMonitor.API.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public ICollection<LogEntry> LogEntries { get; set; } = new List<LogEntry>();
}