using Book.Infrastructure.Repositories.Interfaces;
using Book.Infrastructure.Shared.Exceptions;
using MediatR;

namespace Book.Application.UseCases.Book.DeleteBook;

public class DeleteBookHandler(IBookRepository bookRepository) : IRequestHandler<DeleteBookCommand>
{
    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetFirstByIdAsync(request.bookId, cancellationToken);
        ResourceNotFoundException.ThrowIfNull(book);
        bookRepository.Delete(book);
        await bookRepository.SaveChangesAsync(cancellationToken);
    }
}
