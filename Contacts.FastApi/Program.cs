using Contacts.Data.Database;
using Contacts.Data.Repositories;

using FastEndpoints;
using FastEndpoints.Swagger;
using Serilog;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Http.Json;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        });

    // Register services here
    builder.Services.AddFastEndpoints();
    builder.Services.AddSwaggerDoc();

    // Authentication/Authorization
    builder.Services
        .AddAuthentication(NegotiateDefaults.AuthenticationScheme)
        .AddNegotiate();

    builder.Services
        .AddAuthorization(options =>
        {
            options.FallbackPolicy = options.DefaultPolicy;
        });

    builder.Services
        .AddScoped(typeof(IReadRepository<>), typeof(EfCoreRepository<>))
        .AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));

    builder.Services
        .AddDbContext<ContactsDbContext>(options =>
        {
            var loggerFactory = new LoggerFactory().AddSerilog(Log.Logger);
            options
                .UseLoggerFactory(loggerFactory)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;Initial Catalog=Contacts");
        });

    builder.Services
        .AddOptions<ContextOptions>("ContextOptions") // Dummy method and options class to access dependency injection for DB creation on next method
        .Configure<ILogger<Program>, ContactsDbContext>((options, logger, context) =>
        {
            try
            {
                options.IsInitialized = true;
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Could not create database!");
                options.IsInitialized = false;
            }
        });

    var app = builder.Build();

    app.UseAuthentication();
    app.UseAuthorization();

    // Configure app here
    app.UseFastEndpoints(x => { });

    app.UseOpenApi();
    app.UseSwaggerUi3(s => s.ConfigureDefaults());

    if (!app.Environment.IsProduction())
    {
        app.MapGet("", () => Results.Redirect("/swagger"));
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}