using LogMonitor.API.DTOs;
using LogMonitor.API.Models;
using System.Collections.Generic;
namespace LogMonitor.API.Repositories;

public interface ILogRepository
{
    Task<LogEntry> AddAsync(LogEntry log);
    Task<LogEntry?> GetByIdAsync(long id);
    Task<(IEnumerable<LogEntry> Logs, int TotalCount)> GetLogsAsync(GetLogsRequestDto request);
}