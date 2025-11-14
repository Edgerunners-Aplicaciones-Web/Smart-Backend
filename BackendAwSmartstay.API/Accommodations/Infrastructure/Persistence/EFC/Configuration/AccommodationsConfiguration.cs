using BackendAwSmartstay.API.Accommodations.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAwSmartstay.API.Accommodations.Infrastructure.Persistence.EFC.Configuration;

public class AccommodationsConfiguration : IEntityTypeConfiguration<Accommodation>
{
    public void Configure(EntityTypeBuilder<Accommodation> builder)
    {
        builder.ToTable("accommodations");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.Description)
            .HasMaxLength(1000);

        builder.Property(a => a.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(a => a.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Country)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(a => a.AccommodationType)
            .WithMany(at => at.Accommodations)
            .HasForeignKey(a => a.AccommodationTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.AccommodationSubType)
            .WithMany(ast => ast.Accommodations)
            .HasForeignKey(a => a.AccommodationSubTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.Rooms)
            .WithOne(r => r.Accommodation)
            .HasForeignKey(r => r.AccommodationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class AccommodationTypeConfiguration : IEntityTypeConfiguration<AccommodationType>
{
    public void Configure(EntityTypeBuilder<AccommodationType> builder)
    {
        builder.ToTable("accommodation_types");

        builder.HasKey(at => at.Id);

        builder.Property(at => at.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(at => at.Description)
            .HasMaxLength(500);
    }
}

public class AccommodationSubTypeConfiguration : IEntityTypeConfiguration<AccommodationSubType>
{
    public void Configure(EntityTypeBuilder<AccommodationSubType> builder)
    {
        builder.ToTable("accommodation_sub_types");

        builder.HasKey(ast => ast.Id);

        builder.Property(ast => ast.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ast => ast.Description)
            .HasMaxLength(500);

        builder.HasOne(ast => ast.AccommodationType)
            .WithMany(at => at.AccommodationSubTypes)
            .HasForeignKey(ast => ast.AccommodationTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
{
    public void Configure(EntityTypeBuilder<Amenity> builder)
    {
        builder.ToTable("amenities");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Description)
            .HasMaxLength(500);

        builder.Property(a => a.Icon)
            .HasMaxLength(100);
    }
}

public class AccommodationAmenityConfiguration : IEntityTypeConfiguration<AccommodationAmenity>
{
    public void Configure(EntityTypeBuilder<AccommodationAmenity> builder)
    {
        builder.ToTable("accommodation_amenities");

        builder.HasKey(aa => aa.Id);

        builder.HasOne(aa => aa.Accommodation)
            .WithMany(a => a.AccommodationAmenities)
            .HasForeignKey(aa => aa.AccommodationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(aa => aa.Amenity)
            .WithMany(a => a.AccommodationAmenities)
            .HasForeignKey(aa => aa.AmenityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("rooms");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.RoomNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.Description)
            .HasMaxLength(1000);

        builder.Property(r => r.PricePerNight)
            .HasPrecision(18, 2);

        builder.HasOne(r => r.Accommodation)
            .WithMany(a => a.Rooms)
            .HasForeignKey(r => r.AccommodationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

