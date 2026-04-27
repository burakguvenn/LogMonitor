using LogMonitor.Business.DTOs;

namespace LogMonitor.Business.Extensions;

public static class PaginationExtensions
{
    public static PagedResponseDto<T> ToPagedResponse<T>(
        this IEnumerable<T> source,
        int totalCount,
        int pageNumber,
        int pageSize)
    {
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PagedResponseDto<T>
        {
            Data = source,
            Pagination = new PaginationDto
            {
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = totalPages,
                HasNextPage = pageNumber < totalPages,
                HasPreviousPage = pageNumber > 1
            }
        };
    }
}