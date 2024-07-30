using Book.Application.Dtos.BookDtos;
using Book.Application.SearchParameters;
using MediatR;

namespace Book.Application.UseCases.Book.GetAllBooks;

public record GetAllBooksCommand(BookSearchParameters searchParameters) : IRequest<BookSearchDto>;