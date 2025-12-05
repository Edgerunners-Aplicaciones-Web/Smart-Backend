using BackendAwSmartstay.API.Accommodations.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Accommodations.Domain.Repositories;
using BackendAwSmartstay.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using BackendAwSmartstay.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendAwSmartstay.API.Accommodations.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Repository for the <see cref="Room"/> aggregate that uses Entity Framework Core for persistence.
/// Provides read operations and ensures the <see cref="RoomType"/> navigation property is included.
/// </summary>
public class RoomRepository(AppDbContext context) : BaseRepository<Room>(context), IRoomRepository
{
    /// <summary>
    /// Finds a room by its identifier and includes the related <see cref="RoomType"/>.
    /// </summary>
    /// <param name="id">The identifier of the room to retrieve.</param>
    /// <returns>
    /// A task that resolves to the <see cref="Room"/> with the provided id including its <see cref="RoomType"/>,
    /// or <c>null</c> if no matching entity is found.
    /// </returns>
    public new async Task<Room?> FindByIdAsync(int id)
    {
        return await Context.Set<Room>()
            .Include(r => r.RoomType)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    /// <summary>
    /// Retrieves all rooms from the database including their <see cref="RoomType"/> navigation properties.
    /// </summary>
    /// <returns>A task that resolves to an enumerable of <see cref="Room"/>.</returns>
    public new async Task<IEnumerable<Room>> ListAsync()
    {
        return await Context.Set<Room>()
            .Include(r => r.RoomType)
            .ToListAsync();
    }
}