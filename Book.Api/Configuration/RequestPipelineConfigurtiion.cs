using Book.Application.Configuration;
using Microsoft.AspNetCore.Identity;

namespace Book.Api.Configuration;

public static class RequestPipelineConfiguration
{
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseOpenApi();
        }

        app.UseHttpsRedirection()
            .UseRouting()
            .UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader())
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints => endpoints.MapControllers()); ;

        app.MapGroup("api/identity")
            .MapIdentityApi<IdentityUser<Guid>>();

        return app;
    }
}