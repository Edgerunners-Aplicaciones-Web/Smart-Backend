using BackendAwSmartstay.API.Bookings.Domain.Repositories;
using BackendAwSmartstay.API.Bookings.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BackendAwSmartstay.API.Bookings.Infrastructure.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBookingsContext(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<IBookingRepository, BookingRepository>();

        return services;
    }
}