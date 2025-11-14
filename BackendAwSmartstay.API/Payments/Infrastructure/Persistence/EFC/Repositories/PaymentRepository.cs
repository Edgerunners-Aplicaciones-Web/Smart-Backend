using BackendAwSmartstay.API.Payments.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Payments.Domain.Repositories;
using BackendAwSmartstay.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using BackendAwSmartstay.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace BackendAwSmartstay.API.Payments.Infrastructure.Persistence.EFC.Repositories;


public class PaymentRepository(AppDbContext context) : BaseRepository<Payment>(context), IPaymentRepository;

