namespace BackendAwSmartstay.API.Payments.Interfaces.REST.Resources;

/// <summary>
/// Represents the data returned to the client about a payment.
/// </summary>
public record PaymentResource(
    int Id,
    int BookingId,
    string TransactionId,
    decimal Amount,
    string Status,
    string CardNumberMasked,
    string PaymentDate
);