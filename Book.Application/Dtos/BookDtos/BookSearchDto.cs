namespace Book.Application.Dtos.BookDtos;

public class BookSearchDto
{
    public IEnumerable<BookResponseDto> Books { get; set; }
    public int TotalPages { get; set; }
}
