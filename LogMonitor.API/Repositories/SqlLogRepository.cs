using LogMonitor.API.Data;
using LogMonitor.API.DTOs;
using LogMonitor.API.Models;
using Microsoft.EntityFrameworkCore;
namespace LogMonitor.API.Repositories;

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

    public async Task<LogEntry?> GetByIdAsync(long id)
    {
        return await _context.LogEntries
                .AsNoTracking()
                .FirstOrDefaultAsync(log => log.Id == id);
    }

    public async Task<(IEnumerable<LogEntry> Logs, int TotalCount)> GetLogsAsync(GetLogsRequestDto request)
    {
        var query = _context.LogEntries.AsNoTracking().AsQueryable();

        if (request.Level.HasValue)
        {
            query = query.Where(log => log.Level ==request.Level.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(log => log.CreatedAt >= request.StartDate.Value);
        }

        var totalCount = await query.CountAsync();

        var logs = await query
            .OrderByDescending(log => log.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return (logs, totalCount);
    }
}