using System.Text.Json;
using LogMonitor.Business.DTOs;
using LogMonitor.Entities.Models;

namespace LogMonitor.Business.Extensions;

public static class LogEntryExtensions
{
    public static LogResponseDto ToResponseDto(this LogEntry log)
    {
        return new LogResponseDto
        {
            Id = log.Id,
            Message = log.Message,
            Level = log.Level.ToString(),
            CreatedAt = log.CreatedAt,
            UserId = log.UserId,
            Metadata = log.Metadata != null ? JsonDocument.Parse(log.Metadata) : null
        };
    }

    public static LogEntry ToEntity(this CreateLogDto request, int userId)
    {
        return new LogEntry
        {
            Message = request.Message,
            Level = request.Level,
            UserId = userId,
            Metadata = request.Metadata != null ? JsonSerializer.Serialize(request.Metadata) : null
        };
    }
}