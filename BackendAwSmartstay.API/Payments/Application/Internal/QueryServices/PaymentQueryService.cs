using BackendAwSmartstay.API.Payments.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Payments.Domain.Model.Queries;
using BackendAwSmartstay.API.Payments.Domain.Repositories;
using BackendAwSmartstay.API.Payments.Domain.Services;

namespace BackendAwSmartstay.API.Payments.Application.Internal.QueryServices;

public class PaymentQueryService(IPaymentRepository paymentRepository) : IPaymentQueryService
{
    public async Task<Payment?> Handle(GetPaymentByBookingIdQuery query)
    {
        return await paymentRepository.FindByBookingIdAsync(query.BookingId);
    }
}