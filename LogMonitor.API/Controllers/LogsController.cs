using LogMonitor.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using LogMonitor.API.Models;
using LogMonitor.API.DTOs;
using LogMonitor.API.Models.Enums;

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
        var newLog = new LogEntry
        {
            Message = request.Message,
            Level = request.Level,
            Source = request.Source,
            CreatedAt = DateTime.UtcNow
        };

        var createdLog = await _repository.AddAsync(newLog);

        return CreatedAtAction(nameof(GetByLogId), new {id = createdLog.Id}, createdLog);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByLogId(long id)
    {
        var log = await _repository.GetByIdAsync(id);

        if (log == null) { return NotFound();}

        return Ok(log);
    }

    [HttpGet]
    public async Task<IActionResult> GetLogs([FromQuery] GetLogsRequestDto request)
    {
        var (logs, totalCount) = await _repository.GetLogsAsync(request);

        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var response = new
        {
            Data = logs,
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