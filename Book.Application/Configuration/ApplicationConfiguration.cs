using System.Reflection;
using AutoMapper;
using Book.Application.Options;
using Book.Application.Validators;
using Book.UseCases.UseCases.Author.CreateAuthor;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Book.Application.Configuration;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<S3Settings>(configuration.GetSection("S3Settings"));
        return services
            .AddAutoMapper()
            .AddFluentValidation()
            .AddServices();
    }

    private static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        return services.AddValidatorsFromAssemblyContaining<BookValidator>()
            .AddFluentValidationAutoValidation();
    }

    private static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        var types = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(type => type is { IsClass: true, IsAbstract: false }
                                      && type.IsSubclassOf(typeof(Profile)))
            .ToArray();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(types);
            cfg.DisableConstructorMapping();
        });

        services.AddSingleton(mapperConfig.CreateMapper());

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddMediatR(opt => 
            opt.RegisterServicesFromAssemblyContaining<CreateAuthorCommand>()
        );

        return services;
    }
}