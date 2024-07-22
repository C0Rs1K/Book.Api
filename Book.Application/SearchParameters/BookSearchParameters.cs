namespace Book.Application.SearchParameters;

public class BookSearchParameters
{
    public string? Title { get; set; }
    public string? Genre { get; set; }
    public int? AuthorId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? OwnerName { get; set; }
}