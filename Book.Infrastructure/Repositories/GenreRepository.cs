using Book.Domain.Models;
using Book.Infrastructure.Database;
using Book.Infrastructure.Repositories.Interfaces;

namespace Book.Infrastructure.Repositories;

public class GenreRepository(BookDbContext context) : BaseRepository<GenreModel>(context), IGenreRepository;