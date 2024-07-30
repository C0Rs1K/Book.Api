using Book.Application.Dtos.AuthorDtos;
using MediatR;


namespace Book.UseCases.UseCases.Author.UpdateAuthor;

public record UpdateAuthorCommand(int authorId, AuthorRequestDto updateAuthorDto) : IRequest;
