using LogMonitor.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using LogMonitor.API.Models;
using LogMonitor.API.DTOs;

namespace LogMonitor.API.Controllers;


[ApiController]
[Route("api/[controller]")]

public class LogsController : ControllerBase
{
    private readonly ILogRepository _repository;

    public LogsController(ILogRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Createlog([FromBody] CreateLogDto request)
    {
        var userId = (int)HttpContext.Items["UserId"]!;
        var newLog = new LogEntry
        {
            Message = request.Message,
            Level = request.Level,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        var createdLog = await _repository.AddAsync(newLog);

        var responseDto = new LogResponseDto
        {
            Id = createdLog.Id,
            Message = createdLog.Message,
            Level = createdLog.Level.ToString(),
            CreatedAt = createdLog.CreatedAt,
            UserId = createdLog.UserId
        };

        return CreatedAtAction(nameof(GetByLogId), new {id = responseDto.Id}, responseDto);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByLogId(long id)
    {
        var currentUserId = (int)HttpContext.Items["UserId"]!;
        var log = await _repository.GetByIdAsync(id, currentUserId);

        if (log == null) { return NotFound();}

        var responseDto = new LogResponseDto
        {
            Id = log.Id,
            Message = log.Message,
            Level = log.Level.ToString(),
            CreatedAt = log.CreatedAt,
            UserId = log.UserId
        };

        return Ok(responseDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetLogs([FromQuery] GetLogsRequestDto request)
    {
        var userId = (int)HttpContext.Items["UserId"]!;
        request.UserId = userId;
        var (logs, totalCount) = await _repository.GetLogsAsync(request);

        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var dtoList = logs.Select(log => new LogResponseDto
        {
          Id = log.Id,
          Message = log.Message,
          Level = log.Level.ToString(),
          CreatedAt = log.CreatedAt,
          UserId = log.UserId

        });

        var response = new
        {
            Data = dtoList,
            Pagination = new
            {
                TotalCount = totalCount,
                PageSize = request.PageSize,
                CurrentPage = request.PageNumber,
                TotalPages = totalPages,
                HasNextPage = request.PageNumber < totalPages,
                HasPreviousPage = request.PageNumber > 1
            }
        };

        return Ok(response);
    }
}