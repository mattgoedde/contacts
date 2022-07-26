using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Contacts.Data;
using Contacts.Data.Repositories;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    // Authentication/Authorization
    builder.Services
        .AddAuthentication(NegotiateDefaults.AuthenticationScheme)
        .AddNegotiate();

    builder.Services
        .AddAuthorization(options =>
        {
            options.FallbackPolicy = options.DefaultPolicy;
        });

    // Add services to the container.
    builder.Services
        .AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services
        .AddEndpointsApiExplorer();
    builder.Services
        .AddSwaggerGen();

    builder.Services
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
                options.IsInitialized = DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Could not create database!");
                options.IsInitialized = false;
            }
        });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.UseSerilogRequestLogging();

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