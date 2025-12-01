using BackendAwSmartstay.API.Accommodations.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using BackendAwSmartstay.API.Bookings.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using BackendAwSmartstay.API.Payments.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using BackendAwSmartstay.API.Shared.Infrastructure.Documentation.OpenApi.Configuration.Extensions;
using BackendAwSmartstay.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using BackendAwSmartstay.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using BackendAwSmartstay.API.Shared.Infrastructure.Mediator.Cortex.Configuration.Extensions;
using BackendAwSmartstay.API.IAM.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using BackendAwSmartstay.API.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using BackendAwSmartstay.API.Profiles.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using BackendAwSmartstay.API.shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Database Configuration
builder.AddDatabaseConfigurationServices();

// OpenAPI/Swagger Configuration
builder.AddOpenApiConfigurationServices();

// CORS Configuration
builder.AddCorsServices();

// Dependency Injection
builder.AddSharedContextServices();
builder.AddAccommodationsContextServices();
builder.AddBookingsContextServices();
builder.AddPaymentsContextServices();
builder.AddIamContextServices();
builder.AddProfilesContextServices();

// Mediator Configuration
builder.AddCortexMediatorServices();

var app = builder.Build();

// Verify if the database exists and create it if it doesn't
app.EnsureDatabaseCreated();

// Configure OpenAPI/Swagger middleware
app.UseOpenApiConfiguration();

// Configure CORS middleware
app.UseCorsPolicy();

app.UseHttpsRedirection();

// Configure custom authorization middleware
app.UseRequestAuthorization();

app.MapControllers();
app.Run();