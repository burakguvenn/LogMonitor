using LogMonitor.API.Models.Enums;

namespace LogMonitor.API.DTOs;

public class GetLogsRequestDto
{
    public int PageNumber { get; set; } = 1;

    private int _pageSize = 50;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > 100) ? 100 : value;
    }

    public AppLogLevel? Level { get; set; }

    public DateTime? StartDate { get; set;}
}