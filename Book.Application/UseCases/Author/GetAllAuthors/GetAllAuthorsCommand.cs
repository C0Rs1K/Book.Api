using Book.Application.Dtos.AuthorDtos;
using MediatR;

namespace Book.UseCases.UseCases.Author.GetAllAuthors;

public record GetAllAuthorsCommand : IRequest<IEnumerable<AuthorResponseDto>>;
