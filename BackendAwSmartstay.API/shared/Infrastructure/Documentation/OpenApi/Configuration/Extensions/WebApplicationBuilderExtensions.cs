using Microsoft.OpenApi.Models;
using System.Reflection;
namespace BackendAwSmartstay.API.shared.Infrastructure.Documentation.OpenApi.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddOpenApiConfigurationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "BackendAwSmartstay.API",
                    Version = "v1",
                    Description = "BackendAwSmartstay Hotel Management Platform API",
                    TermsOfService = new Uri("https://acme-learning.com/tos"),
                    Contact = new OpenApiContact
                    {
                        Name = "ACME Studios",
                        Email = "contact@acme.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Apache 2.0",
                        Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                    }
                });
            options.EnableAnnotations();
            
            // Incluir comentarios XML si existen
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath);
            }
        });
    }
}