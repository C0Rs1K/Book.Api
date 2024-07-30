using AutoMapper;
using Book.Application.Dtos.BookDtos;
using Book.Infrastructure.Repositories.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Book.Application.UseCases.Book.GetAllBooks;

public class GetAllBooksHandler(IBookRepository bookRepository, IMapper mapper) : IRequestHandler<GetAllBooksCommand, BookSearchDto>
{
    public async Task<BookSearchDto> Handle(GetAllBooksCommand request, CancellationToken cancellationToken)
    {
        var searchParameters = request.searchParameters;

        var bookModels = bookRepository.GetRange()
            .Where(b => searchParameters.Title == null || b.Name.Contains(searchParameters.Title))
            .Where(b => searchParameters.Genre == null || b.Genre.Name == searchParameters.Genre)
            .Where(b => searchParameters.AuthorId == null || b.AuthorId == searchParameters.AuthorId)
            .Where(b => searchParameters.OwnerName == null || (b.BookOwner != null && b.BookOwner.UserName == searchParameters.OwnerName));

        var totalPages = (bookModels.Count() / searchParameters.PageSize) + 1;

        var books = bookModels
            .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize)
            .Take(searchParameters.PageSize);

        var bookSearch = new BookSearchDto
        {
            Books = await mapper.ProjectTo<BookResponseDto>(books).ToListAsync(cancellationToken),
            TotalPages = totalPages
        };

        return bookSearch;
    }
}
