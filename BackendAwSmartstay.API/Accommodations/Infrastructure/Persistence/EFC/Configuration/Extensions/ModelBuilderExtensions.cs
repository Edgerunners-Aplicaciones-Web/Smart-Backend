using BackendAwSmartstay.API.Accommodations.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Accommodations.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BackendAwSmartstay.API.Accommodations.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyAccommodationsConfiguration(this ModelBuilder builder)
    {
        // RoomType Entity
        builder.Entity<RoomType>().HasKey(rt => rt.Id);
        builder.Entity<RoomType>().Property(rt => rt.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<RoomType>().Property(rt => rt.Name).IsRequired().HasMaxLength(50);
        builder.Entity<RoomType>().Property(rt => rt.Description).IsRequired().HasMaxLength(500);

        // Room Entity
        builder.Entity<Room>().HasKey(r => r.Id);
        builder.Entity<Room>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Room>().Property(r => r.Description).IsRequired().HasMaxLength(1000);
        builder.Entity<Room>().Property(r => r.RoomTypeId).IsRequired();

        // Room Amenities (stored as JSON)
        var converter = new ValueConverter<List<string>, string>(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
            v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>());

        builder.Entity<Room>().Property(r => r.Amenities)
            .HasConversion(converter)
            .HasColumnType("json")
            .IsRequired();

        // Room to RoomType relationship
        builder.Entity<Room>().HasOne(r => r.RoomType)
            .WithMany()
            .HasForeignKey(r => r.RoomTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

