using Book.Application.Services.Interfaces;
using Book.Domain.Models;
using Book.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Book.Application.Services;

public class GenreService(IGenreRepository genreRepository) : IGenreService
{
    public async Task<IEnumerable<string>> GetAllGenresAsync(CancellationToken cancellationToken)
    {
        return await genreRepository.GetRange().Select(x => x.Name).ToListAsync(cancellationToken);
    }

    public async Task<int> CreateGenreAsync(string genreName, CancellationToken cancellationToken)
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
