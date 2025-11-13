

using BackendAwSmartstay.API.shared.Domain.Repositories;
using BackendAwSmartstay.API.shared.Infrastructure.Persistence.EFC.Configuration;

namespace BackendAwSmartstay.API.Shared.Infrastructure.Persistence.EFC.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}