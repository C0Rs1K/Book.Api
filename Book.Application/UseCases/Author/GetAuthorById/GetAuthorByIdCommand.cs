using Book.Application.Dtos.AuthorDtos;
using MediatR;

namespace Book.UseCases.UseCases.Author.GetAuthorById;

public record GetAuthorByIdCommand(int authorId) : IRequest<AuthorResponseDto>;
