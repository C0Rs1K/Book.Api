using Book.Application.Dtos.BookDtos;
using Book.Application.SearchParameters;
using MediatR;

namespace Book.Application.UseCases.Book.GetUserBorrowedBooks;

public record GetUserBorrowedBooksCommand(BookSearchParameters searchParameters, string username) : IRequest<BookSearchDto>;