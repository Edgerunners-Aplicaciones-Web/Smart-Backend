using BackendAwSmartstay.API.Accommodations.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Accommodations.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace BackendAwSmartstay.API.Accommodations.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
/// Extension methods for <see cref="ModelBuilder"/> to apply configuration for the Accommodations Bounded Context.
/// Handles mapping of Aggregates and Entities to the relational database schema.
/// </summary>
public static class ModelBuilderExtensions
{
    /// <summary>
    /// Applies the entity configurations, relationship mappings, and seed data for the Accommodations context.
    /// </summary>
    /// <param name="builder">The model builder instance.</param>
    public static void ApplyAccommodationsConfiguration(this ModelBuilder builder)
    {
        // --- 1. Value Converters ---
        
        // Converter to handle List<string> as a JSON string in the database.
        // This allows storing simple collections (like Amenities) without a separate join table.
        var amenitiesConverter = new ValueConverter<List<string>, string>(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
            v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>());

        // --- 2. Master Data Configuration (Catalogs) ---

        // HotelCategory Configuration
        builder.Entity<HotelCategory>().ToTable("hotel_categories");
        builder.Entity<HotelCategory>().HasKey(c => c.Id);
        builder.Entity<HotelCategory>().Property(c => c.Name).IsRequired().HasMaxLength(50);

        // Seed Data for Hotel Categories
        builder.Entity<HotelCategory>().HasData(
            new HotelCategory { Id = 1, Name = "Hotel" },
            new HotelCategory { Id = 2, Name = "Posada" },
            new HotelCategory { Id = 3, Name = "Lodge" },
            new HotelCategory { Id = 4, Name = "Hostal" },
            new HotelCategory { Id = 5, Name = "Cabaña" },
            new HotelCategory { Id = 6, Name = "Resort" }
        );

        // Amenity Configuration
        builder.Entity<Amenity>().ToTable("amenities_catalog");
        builder.Entity<Amenity>().HasKey(a => a.Id);
        builder.Entity<Amenity>().Property(a => a.Name).IsRequired().HasMaxLength(50);

        // Seed Data for Amenities
        builder.Entity<Amenity>().HasData(
            new Amenity { Id = 1, Name = "Wifi" },
            new Amenity { Id = 2, Name = "Piscina" },
            new Amenity { Id = 3, Name = "Gimnasio" },
            new Amenity { Id = 4, Name = "Restaurante" },
            new Amenity { Id = 5, Name = "Parking" },
            new Amenity { Id = 6, Name = "Spa" },
            new Amenity { Id = 7, Name = "Bar" },
            new Amenity { Id = 8, Name = "Desayuno" }
        );

        // --- 3. Domain Aggregates Configuration ---

        // RoomType Entity
        builder.Entity<RoomType>().ToTable("room_types");
        builder.Entity<RoomType>().HasKey(rt => rt.Id);
        builder.Entity<RoomType>().Property(rt => rt.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<RoomType>().Property(rt => rt.Name).IsRequired().HasMaxLength(50);
        builder.Entity<RoomType>().Property(rt => rt.Description).IsRequired().HasMaxLength(500);

        // Hotel Entity (Aggregate Root)
        builder.Entity<Hotel>().ToTable("hotels");
        builder.Entity<Hotel>().HasKey(h => h.Id);
        builder.Entity<Hotel>().Property(h => h.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Hotel>().Property(h => h.Name).IsRequired().HasMaxLength(100);
        builder.Entity<Hotel>().Property(h => h.Address).IsRequired().HasMaxLength(200);
        builder.Entity<Hotel>().Property(h => h.City).IsRequired().HasMaxLength(100);
        builder.Entity<Hotel>().Property(h => h.Country).IsRequired().HasMaxLength(100);
        builder.Entity<Hotel>().Property(h => h.Description).HasMaxLength(1000);
        
        // Apply JSON converter to Hotel Amenities
        builder.Entity<Hotel>().Property(h => h.Amenities)
            .HasConversion(amenitiesConverter)
            .HasColumnType("json") 
            .IsRequired();

        // Room Entity
        builder.Entity<Room>().ToTable("rooms");
        builder.Entity<Room>().HasKey(r => r.Id);
        builder.Entity<Room>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Room>().Property(r => r.Description).IsRequired().HasMaxLength(1000);
        
        // Monetary value configuration (Precision, Scale)
        builder.Entity<Room>().Property(r => r.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        // Apply JSON converter to Room Amenities
        builder.Entity<Room>().Property(r => r.Amenities)
            .HasConversion(amenitiesConverter)
            .HasColumnType("json")
            .IsRequired();

        // --- 4. Relationship Mappings ---

        builder.Entity<Room>()
            .HasOne(r => r.Hotel)
            .WithMany(h => h.Rooms)
            .HasForeignKey(r => r.HotelId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Room>()
            .HasOne(r => r.RoomType)
            .WithMany()
            .HasForeignKey(r => r.RoomTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}