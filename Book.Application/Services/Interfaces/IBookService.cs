using Book.Application.Dtos.BookDtos;
using Book.Application.SearchParameters;

namespace Book.Application.Services.Interfaces;

public interface IBookService
{
    Task<BookSearchDto> GetAllBooksAsync(BookSearchParameters searchParameters, CancellationToken cancellationToken);
    Task<BookResponseDto> GetBookByIdAsync(int bookId, CancellationToken cancellationToken);
    Task<int> CreateBookAsync(BookRequestDto bookDto, CancellationToken cancellationToken);
    Task UpdateBookAsync(int bookId, BookRequestDto book, CancellationToken cancellationToken);
    Task DeleteBookAsync(int bookId, CancellationToken cancellationToken);
    Task<BookSearchDto> GetUserBorrowedBooksAsync(BookSearchParameters searchParameters, string username, CancellationToken cancellationToken);
}