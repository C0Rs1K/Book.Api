using Book.Domain.Models;
using Book.Infrastructure.Database;
using Book.Infrastructure.Repositories.Interfaces;

namespace Book.Infrastructure.Repositories;

public class BookRepository(BookDbContext context) : BaseRepository<BookModel>(context), IBookRepository;
