using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Book.Application.Configuration;

public static class SwaggerExtensions
{
    public const string VersionV1 = "v1";

    public static string SwaggerJsonPath(string version = VersionV1) => "/swagger/" + version + "/swagger.json";

    public static IServiceCollection AddOpenApi(
        this IServiceCollection services,
        string applicationName,
        Action<OpenApiInfo>? setupOpenApiInfo = null,
        Action<SwaggerGenOptions>? setupGenOption = null,
        string version = VersionV1)
    {
        OpenApiInfo apiInfo = new()
        {
            Version = version,
            Title = applicationName
        };

        setupOpenApiInfo?.Invoke(apiInfo);
        services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            opt.OperationFilter<SecurityRequirementsOperationFilter>();
            opt.DescribeAllParametersInCamelCase();
            opt.SwaggerDoc(version, apiInfo);
            if (setupGenOption is null)
            {
                return;
            }
            setupGenOption(opt);
        });
        return services;
    }

    public static IApplicationBuilder UseOpenApi(
        this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
}