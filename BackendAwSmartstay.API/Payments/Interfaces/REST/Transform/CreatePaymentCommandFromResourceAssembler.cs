using BackendAwSmartstay.API.Payments.Domain.Model.Commands;
using BackendAwSmartstay.API.Payments.Interfaces.REST.Resources;

namespace BackendAwSmartstay.API.Payments.Interfaces.REST.Transform;

public static class CreatePaymentCommandFromResourceAssembler
{
    public static CreatePaymentCommand ToCommandFromResource(CreatePaymentResource resource)
    {
        return new CreatePaymentCommand(resource.BookingId, resource.Amount, resource.PaymentMethod);
    }
}

