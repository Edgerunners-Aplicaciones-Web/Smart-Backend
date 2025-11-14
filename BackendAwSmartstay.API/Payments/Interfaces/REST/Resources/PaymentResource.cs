namespace BackendAwSmartstay.API.Payments.Interfaces.REST.Resources;

public record PaymentResource(int Id, int BookingId, decimal Amount, string PaymentMethod, string Status, DateTime PaymentDate, string? InvoiceNumber);

