using AutoMapper;
using Book.Application.Dtos.BookDtos;
using Book.Application.SearchParameters;
using Book.Application.Services.Interfaces;
using Book.Domain.Models;
using Book.Infrastructure.Repositories.Interfaces;
using Book.Infrastructure.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using static System.Reflection.Metadata.BlobBuilder;

namespace Book.Application.Services;

public class BookService(
    IBookRepository bookRepository,
    IAuthorService authorService,
    IGenreService genreService,
    UserManager<IdentityUser<Guid>> userManager,
    IMapper mapper)
    : IBookService
{
    public async Task<BookResponseDto> GetBookByIdAsync(int bookId, CancellationToken cancellationToken)
    {
        var book = await GetBookModelByIdAsync(bookId, cancellationToken);

        return mapper.Map<BookResponseDto>(book);
    }

    public async Task<BookSearchDto> GetAllBooksAsync(BookSearchParameters searchParameters, CancellationToken cancellationToken)
    {
        return await SearchByParams(searchParameters, cancellationToken);
    }

    public async Task<int> CreateBookAsync(BookRequestDto bookDto, CancellationToken cancellationToken)
    {
        var book = mapper.Map<BookModel>(bookDto);

        book.AuthorId = (await authorService.GetAuthorModelByIdAsync(bookDto.AuthorId, cancellationToken)).Id;
        book.BookOwner = await GetUserByUsernameAsync(bookDto.BookOwner);
        book.GenreId = await genreService.CreateGenreAsync(bookDto.Genre, cancellationToken);

        bookRepository.Create(book);
        await bookRepository.SaveChangesAsync(cancellationToken);

        return book.Id;
    }

    public async Task UpdateBookAsync(int bookId, BookRequestDto bookDto, CancellationToken cancellationToken)
    {
        var book = await GetBookModelByIdAsync(bookId, cancellationToken);

        book.AuthorId = (await authorService.GetAuthorModelByIdAsync(bookDto.AuthorId, cancellationToken)).Id;
        book.BookOwner = await GetUserByUsernameAsync(bookDto.BookOwner);
        book.GenreId = await genreService.CreateGenreAsync(bookDto.Genre, cancellationToken);

        mapper.Map(bookDto, book);

        bookRepository.Update(book);
        await bookRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteBookAsync(int bookId, CancellationToken cancellationToken)
    {
        var book = await GetBookModelByIdAsync(bookId, cancellationToken);

        bookRepository.Delete(book);
        await bookRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<BookSearchDto> GetUserBorrowedBooksAsync(BookSearchParameters searchParameters, string username, CancellationToken cancellationToken)
    {
        searchParameters.OwnerName = username;
        return await SearchByParams(searchParameters, cancellationToken);
    }

    private async Task<BookSearchDto> SearchByParams(BookSearchParameters searchParameters, CancellationToken cancellationToken)
    {
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

    private async Task<BookModel> GetBookModelByIdAsync(int bookId, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetFirstByIdAsync(bookId, cancellationToken);

        if (book == null)
        {
            throw new ResourceNotFoundException(nameof(book));
        }

        return book;
    }

    private async Task<IdentityUser<Guid>> GetUserByUsernameAsync(string username)
    {
        var user = await userManager.FindByNameAsync(username);

        if (user == null)
        {
            throw new ResourceNotFoundException(nameof(user));
        }
        return user;
    }
}