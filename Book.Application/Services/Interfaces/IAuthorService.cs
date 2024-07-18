using Book.Application.Dtos.AuthorDtos;
using Book.Domain.Models;

namespace Book.Application.Services.Interfaces;

public interface IAuthorService
{
    Task<AuthorResponseDto> GetAuthorByIdAsync(int authorId, CancellationToken cancellationToken);
    Task<IEnumerable<AuthorResponseDto>> GetAllAuthorsAsync(CancellationToken cancellationToken);
    Task<int> CreateAuthorAsync(AuthorRequestDto createAuthorDto, CancellationToken cancellationToken);
    Task UpdateAuthorAsync(int authorId, AuthorRequestDto updateAuthorDto, CancellationToken cancellationToken);
    Task DeleteAuthorAsync(int authorId, CancellationToken cancellationToken);
    Task<AuthorModel> GetAuthorModelByIdAsync(int authorId, CancellationToken cancellationToken);
}