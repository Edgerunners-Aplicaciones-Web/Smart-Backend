using BackendAwSmartstay.API.Accommodations.Domain.Entities;
using BackendAwSmartstay.API.Accommodations.Infrastructure.Persistence.EFC.Configuration;
using BackendAwSmartstay.API.shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BackendAwSmartstay.API.shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    // Accommodations Context
    public DbSet<Accommodation> Accommodations => Set<Accommodation>();
    public DbSet<AccommodationType> AccommodationTypes => Set<AccommodationType>();
    public DbSet<AccommodationSubType> AccommodationSubTypes => Set<AccommodationSubType>();
    public DbSet<Amenity> Amenities => Set<Amenity>();
    public DbSet<AccommodationAmenity> AccommodationAmenities => Set<AccommodationAmenity>();
    public DbSet<Room> Rooms => Set<Room>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Accommodations Context Configuration
        builder.ApplyConfiguration(new AccommodationsConfiguration());
        builder.ApplyConfiguration(new AccommodationTypeConfiguration());
        builder.ApplyConfiguration(new AccommodationSubTypeConfiguration());
        builder.ApplyConfiguration(new AmenityConfiguration());
        builder.ApplyConfiguration(new AccommodationAmenityConfiguration());
        builder.ApplyConfiguration(new RoomConfiguration());
        
        
        // General Naming Convention for the database objects
        builder.UseSnakeCaseNamingConvention();
    }
}