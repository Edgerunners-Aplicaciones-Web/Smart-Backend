namespace BackendAwSmartstay.API.Payments.Interfaces.REST.Resources;

public record CreatePaymentResource(int BookingId, decimal Amount, string PaymentMethod);

