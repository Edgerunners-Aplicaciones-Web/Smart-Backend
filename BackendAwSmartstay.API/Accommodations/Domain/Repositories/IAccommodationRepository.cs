using BackendAwSmartstay.API.Accommodations.Domain.Entities;
using BackendAwSmartstay.API.shared.Domain.Repositories;

namespace BackendAwSmartstay.API.Accommodations.Domain.Repositories;

public interface IAccommodationRepository : IBaseRepository<Accommodation>
{
    Task<IEnumerable<Accommodation>> GetByCityAsync(string city);
    Task<IEnumerable<Accommodation>> GetActiveAccommodationsAsync();
    Task<Accommodation?> GetWithRoomsAsync(int id);
    Task<Accommodation?> GetWithAmenitiesAsync(int id);
}