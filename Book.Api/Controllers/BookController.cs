using Book.Application.Dtos.BookDtos;
using Book.Application.SearchParameters;
using Book.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Book.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(IBookService bookService) : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<BookResponseDto>> GetAllBooksAsync([FromQuery] BookSearchParameters searchParameters, CancellationToken cancellationToken)
        {
            return await bookService.GetAllBooksAsync(searchParameters, cancellationToken);
        }

        [HttpGet("{bookId}")]
        public async Task<BookResponseDto> GetBookByIdAsync(int bookId, CancellationToken cancellationToken)
        {
            return await bookService.GetBookByIdAsync(bookId, cancellationToken);
        }

        [HttpPost]
        public async Task<int> CreateBookAsync([FromBody] BookRequestDto bookDto, CancellationToken cancellationToken)
        {
            return await bookService.CreateBookAsync(bookDto, cancellationToken);
        }

        [HttpPut("{bookId}")]
        public async Task UpdateBookAsync(int bookId, [FromBody] BookRequestDto book, CancellationToken cancellationToken)
        {
            await bookService.UpdateBookAsync(bookId, book, cancellationToken);
        }

        [HttpDelete("{bookId}")]
        public async Task DeleteBookAsync(int bookId, CancellationToken cancellationToken)
        {
            await bookService.DeleteBookAsync(bookId, cancellationToken);
        }

        [HttpGet("my")]
        public async Task<IEnumerable<BookResponseDto>> GetUserBorrowedBooksAsync(CancellationToken cancellationToken)
        {
            return await bookService.GetUserBorrowedBooksAsync(User.Identity.Name, cancellationToken);
        }
    }
}
