using BackendAwSmartstay.API.Accommodations.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Accommodations.Interfaces.REST.Resources;

namespace BackendAwSmartstay.API.Accommodations.Interfaces.REST.Transform;

public static class RoomResourceFromEntityAssembler
{

    public static RoomResource ToResourceFromEntity(Room entity)
    {
        return new RoomResource(
            entity.Id,
            entity.RoomTypeId,
            entity.RoomType?.Name ?? string.Empty,
            entity.Description,
            entity.Amenities);
    }
}

