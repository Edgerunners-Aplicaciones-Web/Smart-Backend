using BackendAwSmartstay.API.Payments.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Payments.Domain.Model.Queries;

namespace BackendAwSmartstay.API.Payments.Domain.Services;

public interface IPaymentQueryService
{
    Task<Payment?> Handle(GetPaymentByIdQuery query);
    Task<IEnumerable<Payment>> Handle(GetAllPaymentsQuery query);
    Task<IEnumerable<Payment>> Handle(GetPaymentsByBookingIdQuery query);
}

