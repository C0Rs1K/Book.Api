using Book.Infrastructure.Repositories.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Book.Application.UseCases.Genre.GetAllGenres
{
    public class GetAllGenresHandler(IGenreRepository genreRepository) : IRequestHandler<GetAllGenresCommand, IEnumerable<string>>
    {
        public async Task<IEnumerable<string>> Handle(GetAllGenresCommand request, CancellationToken cancellationToken)
        {
            return await genreRepository.GetRange().Select(x => x.Name).ToListAsync(cancellationToken);
        }
    }
}
