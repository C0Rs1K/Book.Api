using Book.Application.Dtos.AuthorDtos;
using MediatR;

namespace Book.UseCases.UseCases.Author.CreateAuthor;

public record CreateAuthorCommand(AuthorRequestDto createAuthorDto) : IRequest<int>;
