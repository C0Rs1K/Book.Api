using Book.Application.Dtos.BookDtos;
using MediatR;

namespace Book.Application.UseCases.Book.GetBookById;

public record GetBookByIdCommand(int bookId) : IRequest<BookResponseDto>;
