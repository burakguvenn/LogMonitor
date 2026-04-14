using System.Text.Json.Serialization;
using LogMonitor.API.Models.Enums;

namespace LogMonitor.API.DTOs;

public class GetLogsRequestDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; } = 30;
    public AppLogLevel? Level { get; set; }
    public DateTime? StartDate { get; set;}
    [JsonIgnore]
    public int UserId { get; set; }
}