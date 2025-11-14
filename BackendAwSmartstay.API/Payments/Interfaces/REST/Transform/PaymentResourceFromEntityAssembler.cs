using BackendAwSmartstay.API.Payments.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Payments.Interfaces.REST.Resources;

namespace BackendAwSmartstay.API.Payments.Interfaces.REST.Transform;

public static class PaymentResourceFromEntityAssembler
{

    public static PaymentResource ToResourceFromEntity(Payment entity)
    {
        return new PaymentResource(
            entity.Id,
            entity.BookingId,
            entity.Amount,
            entity.PaymentMethod,
            entity.Status.ToString(),
            entity.PaymentDate,
            entity.InvoiceNumber);
    }
}

