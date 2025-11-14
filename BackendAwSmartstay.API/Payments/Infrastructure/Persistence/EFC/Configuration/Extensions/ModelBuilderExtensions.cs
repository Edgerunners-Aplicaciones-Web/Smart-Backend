using BackendAwSmartstay.API.Payments.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace BackendAwSmartstay.API.Payments.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyPaymentsConfiguration(this ModelBuilder builder)
    {
        // Payment Entity
        builder.Entity<Payment>().HasKey(p => p.Id);
        builder.Entity<Payment>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Payment>().Property(p => p.BookingId).IsRequired();
        builder.Entity<Payment>().Property(p => p.Amount).IsRequired().HasPrecision(18, 2);
        builder.Entity<Payment>().Property(p => p.PaymentMethod).IsRequired().HasMaxLength(50);
        builder.Entity<Payment>().Property(p => p.PaymentDate).IsRequired();
        builder.Entity<Payment>().Property(p => p.InvoiceNumber).HasMaxLength(50);
        builder.Entity<Payment>().Property(p => p.Status)
            .HasConversion<int>()
            .IsRequired();
    }
}

