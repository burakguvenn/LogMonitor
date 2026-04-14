using LogMonitor.API.DTOs;
using LogMonitor.API.Models;
namespace LogMonitor.API.Repositories;

public interface ILogRepository
{
    Task<LogEntry> AddAsync(LogEntry log);
    Task<LogEntry?> GetByIdAsync(long id, int currentUserId);
    Task<(IEnumerable<LogEntry> Logs, int TotalCount)> GetLogsAsync(GetLogsRequestDto request);
}