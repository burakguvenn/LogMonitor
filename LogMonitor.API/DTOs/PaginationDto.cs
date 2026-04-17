using System.Text.Json.Serialization;

namespace LogMonitor.API.DTOs;

public class PaginationDto
{
    public int TotalCount { get; set; }
    [JsonIgnore]
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }
}