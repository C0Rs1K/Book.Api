using Book.Application.Dtos.AuthorDtos;
using Book.UseCases.UseCases.Author.CreateAuthor;
using Book.UseCases.UseCases.Author.DeleteAuthor;
using Book.UseCases.UseCases.Author.GetAllAuthors;
using Book.UseCases.UseCases.Author.GetAuthorById;
using Book.UseCases.UseCases.Author.UpdateAuthor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Book.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController(ISender sender) : ControllerBase
    {
        [HttpGet("{authorId}")]
        [ProducesResponseType<AuthorRequestDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAuthorByIdAsync(int authorId, CancellationToken cancellationToken)
        {
            return Ok(await sender.Send(new GetAuthorByIdCommand(authorId), cancellationToken));
        }

        [HttpGet]
        [ProducesResponseType<IEnumerable<AuthorRequestDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllAuthorsAsync(CancellationToken cancellationToken)
        {
            return Ok(await sender.Send(new GetAllAuthorsCommand(), cancellationToken));
        }

        [HttpPost]
        [ProducesResponseType<int>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAuthorAsync([FromBody] AuthorRequestDto createAuthorDto, CancellationToken cancellationToken)
        {
            return CreatedAtAction(nameof(CreateAuthorAsync), await sender.Send(new CreateAuthorCommand(createAuthorDto), cancellationToken));
        }

        [HttpPut("{authorId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAuthorAsync(int authorId, [FromBody] AuthorRequestDto updateAuthorDto, CancellationToken cancellationToken)
        {
            await sender.Send(new UpdateAuthorCommand(authorId, updateAuthorDto), cancellationToken);
            return NoContent();
        }

        [HttpDelete("{authorId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAuthorAsync(int authorId, CancellationToken cancellationToken)
        {
            await sender.Send(new DeleteAuthorCommand(authorId), cancellationToken);
            return NoContent();
        }
    }
}
