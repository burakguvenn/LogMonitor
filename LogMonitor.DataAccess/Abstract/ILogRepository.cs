using LogMonitor.Entities.Models;
using LogMonitor.Entities.Models.Enums;

namespace LogMonitor.DataAccess.Abstract;

public interface ILogRepository
{
    Task<LogEntry> AddAsync(LogEntry log);
    Task<LogEntry?> GetByIdAsync(long id, int currentUserId);
    Task<(IEnumerable<LogEntry> Logs, int TotalCount)> GetLogsAsync(int userId, AppLogLevel? level, DateTime? startDate, int pageNumber, int pageSize);
}