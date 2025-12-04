using BackendAwSmartstay.API.Payments.Domain.Model.Commands;

namespace BackendAwSmartstay.API.Payments.Domain.Model.Aggregates;

/// <summary>
/// Represents a payment transaction within the system.
/// Contains details about the amount, method, status, and associated booking.
/// </summary>
public partial class Payment
{
    public Payment()
    {
        TransactionId = Guid.NewGuid().ToString();
        PaymentMethod = string.Empty;
        CardHolderName = string.Empty;
        CardNumberMasked = string.Empty;
        Status = PaymentStatus.Pending;
        PaymentDate = DateTime.UtcNow;
    }

    public Payment(ProcessPaymentCommand command) : this()
    {
        BookingId = command.BookingId;
        Amount = command.Amount;
        PaymentMethod = command.PaymentMethod; // e.g., "Credit Card"
        CardHolderName = command.CardHolderName;
        // Solo guardamos los últimos 4 dígitos por seguridad (PCI Compliance simulado)
        CardNumberMasked = command.CardNumber.Length > 4 
            ? "**** **** **** " + command.CardNumber.Substring(command.CardNumber.Length - 4) 
            : "****";
    }

    public int Id { get; }
    public int BookingId { get; private set; }
    public string TransactionId { get; private set; } // UUID único de la transacción
    public decimal Amount { get; private set; }
    public string PaymentMethod { get; private set; }
    public string CardHolderName { get; private set; }
    public string CardNumberMasked { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public PaymentStatus Status { get; private set; }

    /// <summary>
    /// Marks the payment as successfully completed.
    /// </summary>
    public void Complete()
    {
        Status = PaymentStatus.Completed;
    }

    /// <summary>
    /// Marks the payment as failed.
    /// </summary>
    public void Fail()
    {
        Status = PaymentStatus.Failed;
    }
}

public enum PaymentStatus
{
    Pending = 0,
    Completed = 1,
    Failed = 2
}