using Book.Application.Dtos.BookDtos;
using MediatR;

namespace Book.Application.UseCases.Book.CreateBook;

public record CreateBookCommand(BookRequestDto bookDto) : IRequest<int>;
