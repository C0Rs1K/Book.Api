using MediatR;

namespace Book.UseCases.UseCases.Author.DeleteAuthor;

public record DeleteAuthorCommand(int authorId) : IRequest;
