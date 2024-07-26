using Book.Application.Dtos.BookDtos;
using Book.Application.SearchParameters;
using Book.Application.UseCases.Book.CreateBook;
using Book.Application.UseCases.Book.DeleteBook;
using Book.Application.UseCases.Book.GetAllBooks;
using Book.Application.UseCases.Book.GetBookById;
using Book.Application.UseCases.Book.GetUserBorrowedBooks;
using Book.Application.UseCases.Book.UpdateBook;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Book.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(ISender sender) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<BookSearchDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllBooksAsync([FromQuery] BookSearchParameters searchParameters, CancellationToken cancellationToken)
        {
            return Ok(await sender.Send(new GetAllBooksCommand(searchParameters), cancellationToken));
        }

        [HttpGet("{bookId}")]
        [ProducesResponseType<BookResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBookByIdAsync(int bookId, CancellationToken cancellationToken)
        {
            return Ok(await sender.Send(new GetBookByIdCommand(bookId), cancellationToken));
        }

        [HttpPost]
        [ProducesResponseType<int>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateBookAsync([FromBody] BookRequestDto bookDto, CancellationToken cancellationToken)
        {
            return CreatedAtAction(nameof(CreateBookAsync), await sender.Send(new CreateBookCommand(bookDto), cancellationToken));
        }

        [HttpPut("{bookId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBookAsync(int bookId, [FromBody] BookRequestDto book, CancellationToken cancellationToken)
        {
            await sender.Send(new UpdateBookCommand(bookId, book), cancellationToken);
            return NoContent();
        }

        [HttpDelete("{bookId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBookAsync(int bookId, CancellationToken cancellationToken)
        {
            await sender.Send(new DeleteBookCommand(bookId), cancellationToken);
            return NoContent();
        }

        [HttpGet("MyBooks")]
        [ProducesResponseType<BookSearchDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserBorrowedBooksAsync([FromQuery] BookSearchParameters searchParameters, CancellationToken cancellationToken)
        {
            return Ok(await sender.Send(new GetUserBorrowedBooksCommand(searchParameters, User.Identity.Name), cancellationToken));
        }
    }
}
