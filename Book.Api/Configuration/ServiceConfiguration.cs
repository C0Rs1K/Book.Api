using Book.Api.Middleware;
using Book.Application.Configuration;
using Book.Infrastructure.Configuration;
using Book.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;

namespace Book.Api.Configuration;

public static class ServiceConfiguration
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("BookDatabase");

        builder.Services
            .AddApplication(builder.Configuration)
            .AddInfrastructure(connectionString ?? throw new NullReferenceException("Connection string is null."));

        builder.Services
            .AddOpenApi(builder.Environment.ApplicationName)
            .AddCors()
            .AddEndpointsApiExplorer()
            .AddControllers(opt => opt.Filters.Add<HttpGlobalExceptionFilter>())
            .AddJsonSerializer();

        builder.Services.ConfigureIdentity();
    }

    private static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {


        services.AddAuthorization()
            .AddIdentityApiEndpoints<IdentityUser<Guid>>()
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<BookDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}