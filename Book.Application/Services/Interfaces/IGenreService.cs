namespace Book.Application.Services.Interfaces;

public interface IGenreService
{
    Task<IEnumerable<string>> GetAllGenresAsync(CancellationToken cancellationToken);
    Task<int> CreateGenreAsync(string genreName, CancellationToken cancellationToken);
}
