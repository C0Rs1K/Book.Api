using AutoMapper;
using Book.Domain.Models;
using Book.Infrastructure.Repositories;
using Book.Infrastructure.Repositories.Interfaces;
using Book.Infrastructure.Shared.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Book.Application.UseCases.Book.CreateBook;

public class CreateBookHandler(IBookRepository bookRepository
    , IAuthorRepository authorRepository
    , IGenreRepository genreRepository
    , UserManager<IdentityUser<Guid>> userManager
    , IMapper mapper) 
    : IRequestHandler<CreateBookCommand, int>
{
    public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var bookDto = request.bookDto;
        var book = mapper.Map<BookModel>(bookDto);

        var author = await authorRepository.GetFirstByIdAsync(bookDto.AuthorId, cancellationToken);
        ResourceNotFoundException.ThrowIfNull(author);
        book.AuthorId = author.Id;

        book.BookOwner = await userManager.FindByNameAsync(bookDto.BookOwner);
        ResourceNotFoundException.ThrowIfNull(book.BookOwner);

        book.GenreId = await CreateGenreAsync(bookDto.Genre, cancellationToken);

        bookRepository.Create(book);
        await bookRepository.SaveChangesAsync(cancellationToken);

        return book.Id;
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
