using Book.Api.Configuration;
using Serilog;

LoggerConfig.ConfigureLogger();

try
{
    Log.Information("Application Start");
    var builder = WebApplication.CreateBuilder(args);
    builder.ConfigureServices();
    var app = builder.Build();
    app.ConfigurePipeline();
    app.Run();
}
catch (Exception exception)
{
    Log.Fatal(exception, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}