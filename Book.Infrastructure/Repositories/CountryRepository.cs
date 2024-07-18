using Book.Domain.Models;
using Book.Infrastructure.Database;
using Book.Infrastructure.Repositories.Interfaces;

namespace Book.Infrastructure.Repositories;

public class CountryRepository(BookDbContext context) : BaseRepository<CountryModel>(context), ICountryRepository;