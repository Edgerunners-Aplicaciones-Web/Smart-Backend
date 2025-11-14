using BackendAwSmartstay.API.Payments.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Shared.Domain.Repositories;

namespace BackendAwSmartstay.API.Payments.Domain.Repositories;

public interface IPaymentRepository : IBaseRepository<Payment>
{
}

