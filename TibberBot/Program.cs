using Npgsql;
using Serilog;
using TibberBot.Cleaners;
using TibberBot.Repository;

namespace TibberBot
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddEnvironmentVariables()
            .Build();

        public static void Main(string[] args)
        {
            var loggerConfiguration = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate:
                    "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level:u3}] {Message:lj}{NewLine}{Exception}");

            Log.Logger = loggerConfiguration.CreateLogger();
            Log.Information("Starting TibberBot...");

            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog();

            // Add services to the container.
            var executionsConnString = builder.Configuration.GetConnectionString("Executions");
            if (string.IsNullOrEmpty(executionsConnString))
                throw new InvalidOperationException("Executions connection string missing");

            builder.Services.AddNpgsqlDataSource(executionsConnString);
            builder.Services.AddSingleton<IExecutionsRepository>(
                provider => new ExecutionsRepository(provider.GetRequiredService<ILogger<ExecutionsRepository>>(), provider.GetRequiredService<NpgsqlDataSource>())
            );
            builder.Services.AddSingleton<ICleaner>(
                provider => new OfficeCleaner(provider.GetRequiredService<ILogger<OfficeCleaner>>())
            );
            builder.Services.AddControllers();
            builder.Services.AddHealthChecks();

            var app = builder.Build();
            app.UseAuthorization();
            app.MapControllers();
            app.MapHealthChecks("/health");
            app.Run();
        }
    }
}