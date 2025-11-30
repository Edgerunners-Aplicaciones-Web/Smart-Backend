using BackendAwSmartstay.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BackendAwSmartstay.API.shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddDatabaseConfigurationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            if (builder.Environment.IsDevelopment())
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(connectionString)) 
                    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            }
            else if (builder.Environment.IsProduction())
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();

                var connectionStringTemplate = configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(connectionStringTemplate))
                    throw new Exception("Database connection string template is not set in the configuration.");

                var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);
                if (string.IsNullOrEmpty(connectionString))
                    throw new Exception("Database connection string is not set in the configuration.");

                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Error)
                    .EnableDetailedErrors();
            }
        });
    }
}