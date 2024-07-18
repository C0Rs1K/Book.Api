using Book.Application.Dtos.AuthorDtos;
using Book.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController(IAuthorService authorService) : ControllerBase
    {
        [HttpGet("{authorId}")]
        public async Task<AuthorResponseDto> GetAuthorByIdAsync(int authorId, CancellationToken cancellationToken)
        {
            return await authorService.GetAuthorByIdAsync(authorId, cancellationToken);
        }

        [HttpGet]
        public async Task<IEnumerable<AuthorResponseDto>> GetAllAuthorsAsync(CancellationToken cancellationToken)
        {
            return await authorService.GetAllAuthorsAsync(cancellationToken);
        }

        [HttpPost]
        public async Task<int> CreateAuthorAsync([FromBody] AuthorRequestDto createAuthorDto, CancellationToken cancellationToken)
        {
            return await authorService.CreateAuthorAsync(createAuthorDto, cancellationToken);
        }

        [HttpPut("{authorId}")]
        public async Task UpdateAuthorAsync(int authorId, [FromBody] AuthorRequestDto updateAuthorDto, CancellationToken cancellationToken)
        {
            await authorService.UpdateAuthorAsync(authorId, updateAuthorDto, cancellationToken);
        }

        [HttpDelete("{authorId}")]
        public async Task DeleteAuthorAsync(int authorId, CancellationToken cancellationToken)
        {
            await authorService.DeleteAuthorAsync(authorId, cancellationToken);
        }
    }
}
