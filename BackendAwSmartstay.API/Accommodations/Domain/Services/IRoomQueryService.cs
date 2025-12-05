using BackendAwSmartstay.API.Accommodations.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Accommodations.Domain.Model.Queries;

namespace BackendAwSmartstay.API.Accommodations.Domain.Services;

/// <summary>
/// Query service for read-only operations related to <see cref="Room"/>.
/// Implementations should resolve the queries and return the corresponding results.
/// </summary>
public interface IRoomQueryService
{
    /// <summary>
    /// Handles the query to get a room by its identifier.
    /// </summary>
    /// <param name="query">Query containing the identifier of the room to retrieve.</param>
    /// <returns>
    /// A task that, when completed, returns the <see cref="Room"/> found, or <c>null</c> if not found.
    /// </returns>
    Task<Room?> Handle(GetRoomByIdQuery query);

    /// <summary>
    /// Handles the query to get all rooms that match the query criteria.
    /// </summary>
    /// <param name="query">Query that can include filters or search parameters.</param>
    /// <returns>A task that, when completed, returns a collection of <see cref="Room"/>.</returns>
    Task<IEnumerable<Room>> Handle(GetAllRoomsQuery query);

    /// <summary>
    /// Handles the query to get rooms filtered by type.
    /// </summary>
    /// <param name="query">Query that specifies the room type to retrieve.</param>
    /// <returns>A task that, when completed, returns a collection of <see cref="Room"/> matching the requested type.</returns>
    Task<IEnumerable<Room>> Handle(GetRoomsByTypeQuery query);
}