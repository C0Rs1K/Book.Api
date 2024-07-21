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

namespace Book.Application.Services;

public class BookService(
    IBookRepository bookRepository,
    IAuthorService authorService,
    IGenreRepository genreRepository,
    UserManager<IdentityUser<Guid>> userManager,
    IMapper mapper)
    : IBookService
{
    public async Task<BookResponseDto> GetBookByIdAsync(int bookId, CancellationToken cancellationToken)
    {
        var book = await GetBookModelByIdAsync(bookId, cancellationToken);

        return mapper.Map<BookResponseDto>(book);
    }

    public async Task<IEnumerable<BookResponseDto>> GetAllBooksAsync(BookSearchParameters searchParameters, CancellationToken cancellationToken)
    {
        var books = bookRepository.GetRange()
            .Where(b => searchParameters.Title == null || b.Name.Contains(searchParameters.Title))
            .Where(b => searchParameters.Genre == null || b.Genre.Name == searchParameters.Genre)
            .Where(b => searchParameters.AuthorId == null || b.AuthorId == searchParameters.AuthorId)
            .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize)
            .Take(searchParameters.PageSize);

        return await mapper.ProjectTo<BookResponseDto>(books).ToListAsync(cancellationToken);
    }

    public async Task<int> CreateBookAsync(BookRequestDto bookDto, CancellationToken cancellationToken)
    {
        var book = mapper.Map<BookModel>(bookDto);

        book.AuthorId = (await authorService.GetAuthorModelByIdAsync(bookDto.AuthorId, cancellationToken)).Id;
        book.BookOwner = await GetUserByUsernameAsync(bookDto.BookOwner);
        book.GenreId = await CreateGenreAsync(bookDto.Genre, cancellationToken);

        bookRepository.Create(book);
        await bookRepository.SaveChangesAsync(cancellationToken);

        return book.Id;
    }

    public async Task UpdateBookAsync(int bookId, BookRequestDto bookDto, CancellationToken cancellationToken)
    {
        var book = await GetBookModelByIdAsync(bookId, cancellationToken);

        book.AuthorId = (await authorService.GetAuthorModelByIdAsync(bookDto.AuthorId, cancellationToken)).Id;
        book.BookOwner = await GetUserByUsernameAsync(bookDto.BookOwner);
        book.GenreId = await CreateGenreAsync(bookDto.Genre, cancellationToken);

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

    public async Task<IEnumerable<BookResponseDto>> GetUserBorrowedBooksAsync(string username, CancellationToken cancellationToken)
    {
        var bookTakes = bookRepository.GetRange()
            .Where(bt => bt.BookOwner != null && bt.BookOwner.UserName == username);

        return await mapper.ProjectTo<BookResponseDto>(bookTakes).ToListAsync(cancellationToken);
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

    private async Task<int> CreateGenreAsync(string genreName, CancellationToken cancellationToken)
    {
        var genre = await genreRepository.GetRange()
            .FirstOrDefaultAsync(x => x.Name == genreName, cancellationToken);

        if (genre == null)
        {
            genre = new GenreModel
            {
                Name = genreName,
            };
            genreRepository.Create(genre);

            await genreRepository.SaveChangesAsync(cancellationToken);
        }

        return genre.Id;
    }
}