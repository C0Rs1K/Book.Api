using MediatR;

namespace Book.Application.UseCases.Book.DeleteBook;

public record DeleteBookCommand(int bookId) : IRequest;