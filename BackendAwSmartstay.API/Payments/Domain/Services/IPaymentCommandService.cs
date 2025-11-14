using BackendAwSmartstay.API.Payments.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Payments.Domain.Model.Commands;

namespace BackendAwSmartstay.API.Payments.Domain.Services;

public interface IPaymentCommandService
{

    public Task<Payment?> Handle(CreatePaymentCommand command);

    public Task<Payment?> Handle(ProcessPaymentCommand command);

    public Task<Payment?> Handle(FailPaymentCommand command);
}

