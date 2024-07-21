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

        app.MapGroup("api/identity").MapIdentityApi<IdentityUser<Guid>>();

        app.UseHttpsRedirection()
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints => endpoints.MapControllers());

        return app;
    }
}