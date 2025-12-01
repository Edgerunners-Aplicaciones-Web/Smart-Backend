using BackendAwSmartstay.API.Accommodations.Infrastructure.Persistence.EFC.Configuration.Extensions;
using BackendAwSmartstay.API.Bookings.Infrastructure.Persistence.EFC.Configuration.Extensions;
using BackendAwSmartstay.API.Payments.Infrastructure.Persistence.EFC.Configuration.Extensions;
using BackendAwSmartstay.API.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;
using BackendAwSmartstay.API.Profiles.Infrastructure.Persistence.EFC.Configuration.Extensions;
using BackendAwSmartstay.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BackendAwSmartstay.API.Shared.Infrastructure.Persistence.EFC.Configuration;


public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // IAM Context
        builder.ApplyIamConfiguration();

        // Profiles Context
        builder.ApplyProfilesConfiguration();

        // Accommodations Context
        builder.ApplyAccommodationsConfiguration();

        // Bookings Context
        builder.ApplyBookingsConfiguration();

        // Payments Context
        builder.ApplyPaymentsConfiguration();

        // General Naming Convention for the database objects
        builder.UseSnakeCaseNamingConvention();
    }
}

