using LogMonitor.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using LogMonitor.API.DTOs;
using LogMonitor.API.Extensions;

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

        var newLog = request.ToEntity(userId);

        var createdLog = await _repository.AddAsync(newLog);

        var responseDto = createdLog.ToResponseDto();

        return CreatedAtAction(nameof(GetByLogId), new {id = responseDto.Id}, responseDto);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByLogId(long id)
    {
        var currentUserId = (int)HttpContext.Items["UserId"]!;

        var log = await _repository.GetByIdAsync(id, currentUserId);

        if (log == null) { return NotFound();}

        var responseDto = log.ToResponseDto();

        return Ok(responseDto);
    }

    [HttpGet]
    public async Task<IActionResult> GetLogs([FromQuery] GetLogsRequestDto request)
    {
        var userId = (int)HttpContext.Items["UserId"]!;

        request.UserId = userId;

        var (logs, totalCount) = await _repository.GetLogsAsync(request);

        var dtoList = logs.Select(log => log.ToResponseDto());

        var response = dtoList.ToPagedResponse(totalCount, request.PageNumber, request.PageSize);

        return Ok(response);
    }
}