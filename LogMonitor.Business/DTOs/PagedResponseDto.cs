using System.Text.Json;

namespace LogMonitor.Business.DTOs;

public class PagedResponseDto<T>
{
    public IEnumerable<T> Data { get; set; } = [];
    public PaginationDto Pagination { get; set; } = null!;
}