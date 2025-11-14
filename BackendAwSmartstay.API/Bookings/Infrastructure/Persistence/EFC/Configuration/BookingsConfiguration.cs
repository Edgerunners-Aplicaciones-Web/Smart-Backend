using BackendAwSmartstay.API.Bookings.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAwSmartstay.API.Bookings.Infrastructure.Persistence.EFC.Configuration;

public class BookingsConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("bookings");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.NumberOfGuests)
            .IsRequired();

        builder.Property(b => b.TotalAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(b => b.SpecialRequests)
            .HasMaxLength(1000);

        builder.HasOne(b => b.BookingStatus)
            .WithMany(bs => bs.Bookings)
            .HasForeignKey(b => b.BookingStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class BookingStatusConfiguration : IEntityTypeConfiguration<BookingStatus>
{
    public void Configure(EntityTypeBuilder<BookingStatus> builder)
    {
        builder.ToTable("booking_statuses");

        builder.HasKey(bs => bs.Id);

        builder.Property(bs => bs.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(bs => bs.Description)
            .HasMaxLength(500);
    }
}