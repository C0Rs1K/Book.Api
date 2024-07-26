using Book.Application.Dtos.BookDtos;
using MediatR;

namespace Book.Application.UseCases.Book.UpdateBook;

public record UpdateBookCommand(int bookId, BookRequestDto bookDto) : IRequest;