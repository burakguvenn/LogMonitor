using LogMonitor.DataAccess.Abstract;
using LogMonitor.DataAccess.Concrete.Context;
using LogMonitor.Entities.Models;
using LogMonitor.Entities.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace LogMonitor.DataAccess.Concrete.Repositories;

public class SqlLogRepository : ILogRepository
{
    private readonly AppDbContext _context;

    public SqlLogRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LogEntry> AddAsync(LogEntry log)
    {
        _context.LogEntries.Add(log);
        await _context.SaveChangesAsync();
        return log;
    }

    public async Task<LogEntry?> GetByIdAsync(long id, int currentUserId)
    {
        return await _context.LogEntries
                .AsNoTracking()
                .FirstOrDefaultAsync(log => log.Id == id && log.UserId == currentUserId);
    }

    public async Task<(IEnumerable<LogEntry> Logs, int TotalCount)> GetLogsAsync(int userId, AppLogLevel? level, DateTime? startDate, int pageNumber, int pageSize)
    {
        var query = _context.LogEntries.AsNoTracking().AsQueryable();

        query = query.Where(log => log.UserId == userId); // sadece bu kullanıcıya ait log'ları alır. Diğerleri bu sorguda gözükmez.

        if (level.HasValue)
        {
            query = query.Where(log => log.Level == level.Value);
        }

        if (startDate.HasValue)
        {
            query = query.Where(log => log.CreatedAt >= startDate.Value);
        }

        var totalCount = await query.CountAsync();

        var logs = await query
            .OrderByDescending(log => log.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (logs, totalCount);
    }
}