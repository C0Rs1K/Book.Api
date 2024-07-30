using MediatR;

namespace Book.Application.UseCases.Genre.GetAllGenres
{
    public record GetAllGenresCommand : IRequest<IEnumerable<string>>;
}
