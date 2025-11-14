using BackendAwSmartstay.API.Accommodations.Domain.Repositories;
using BackendAwSmartstay.API.Accommodations.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BackendAwSmartstay.API.Accommodations.Infrastructure.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAccommodationsContext(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<IAccommodationRepository, AccommodationRepository>();

        return services;
    }
}