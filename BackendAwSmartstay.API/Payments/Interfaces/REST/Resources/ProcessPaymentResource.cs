using System.ComponentModel.DataAnnotations;

namespace BackendAwSmartstay.API.Payments.Interfaces.REST.Resources;

/// <summary>
/// Represents the data required to process a payment.
/// </summary>
public record ProcessPaymentResource(
    [Required] int BookingId,
    [Required] decimal Amount,
    [Required] string PaymentMethod,
    [Required] [CreditCard] string CardNumber,
    [Required] string CardHolderName,
    [Required] string ExpirationDate,
    [Required] string Cvv
);