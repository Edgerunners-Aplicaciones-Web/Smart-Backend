using BackendAwSmartstay.API.Accommodations.Domain.Entities;
using BackendAwSmartstay.API.Accommodations.Domain.Repositories;
using BackendAwSmartstay.API.shared.Infrastructure.Persistence.EFC.Configuration;
using BackendAwSmartstay.API.shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendAwSmartstay.API.Accommodations.Infrastructure.Persistence.Repositories;

public class AccommodationRepository : BaseRepository<Accommodation>, IAccommodationRepository
{
    public AccommodationRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Accommodation>> GetByCityAsync(string city)
    {
        return await Context.Set<Accommodation>()
            .Where(a => a.City == city && a.IsActive)
            .ToListAsync();
    }

    public async Task<IEnumerable<Accommodation>> GetActiveAccommodationsAsync()
    {
        return await Context.Set<Accommodation>()
            .Where(a => a.IsActive)
            .ToListAsync();
    }

    public async Task<Accommodation?> GetWithRoomsAsync(int id)
    {
        return await Context.Set<Accommodation>()
            .Include(a => a.Rooms)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Accommodation?> GetWithAmenitiesAsync(int id)
    {
        return await Context.Set<Accommodation>()
            .Include(a => a.AccommodationAmenities)
            .ThenInclude(aa => aa.Amenity)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}