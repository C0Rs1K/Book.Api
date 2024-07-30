using Book.Infrastructure.Repositories.Interfaces;
using Book.Infrastructure.Shared.Exceptions;
using MediatR;

namespace Book.UseCases.UseCases.Author.DeleteAuthor;

public class DeleteAuthorHandler(IAuthorRepository authorRepository) : IRequestHandler<DeleteAuthorCommand>
{
    public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await authorRepository.GetFirstByIdAsync(request.authorId, cancellationToken);
        ResourceNotFoundException.ThrowIfNull(author);
        authorRepository.Delete(author);
        await authorRepository.SaveChangesAsync(cancellationToken);
    }
}
