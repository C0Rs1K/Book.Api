using Book.Infrastructure.Database;
using Book.Infrastructure.Repositories;
using Book.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Book.Infrastructure.Configuration;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services
            .AddDatabase(connectionString)
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.
            AddDbContext<BookDbContext>(dbOptions =>
            {
                dbOptions.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(BookDbContext).Assembly.FullName);
                });
            });

        return services;
    }
}