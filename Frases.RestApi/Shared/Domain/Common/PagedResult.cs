namespace FrasesApi.Shared.Domain.Common;

public class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; init; } = new List<T>();
    public int TotalCount { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasNextPage => PageNumber < TotalPages;
    public bool HasPreviousPage => PageNumber > 1;
}

public class PaginationRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public PaginationRequest()
    {
        PageNumber = PageNumber < 1 ? 1 : PageNumber;
        PageSize = PageSize > 50 ? 50 : PageSize < 1 ? 10 : PageSize;
    }
}


public class CursorPagedResult<T>
{
    public IReadOnlyList<T> Items { get; init; } = new List<T>();
    public string? NextCursor { get; init; }
    public string? PreviousCursor { get; init; }
    public bool HasNextPage { get; init; }
    public bool HasPreviousPage { get; init; }
    public int PageSize { get; init; }
}

public class CursorPaginationRequest
{
    public string? Cursor { get; set; }
    public int PageSize { get; set; } = 10;
    public PaginationDirection Direction { get; set; } = PaginationDirection.Forward;
    
    public CursorPaginationRequest()
    {
        PageSize = PageSize > 50 ? 50 : PageSize < 1 ? 10 : PageSize;
    }
}

public enum PaginationDirection
{
    Forward,
    Backward
}