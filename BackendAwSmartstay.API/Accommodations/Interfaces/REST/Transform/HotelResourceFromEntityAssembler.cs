using BackendAwSmartstay.API.Accommodations.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Accommodations.Interfaces.REST.Resources;

namespace BackendAwSmartstay.API.Accommodations.Interfaces.REST.Transform;

public static class HotelResourceFromEntityAssembler
{
    public static HotelResource ToResourceFromEntity(Hotel entity)
    {
        return new HotelResource(
            entity.Id,
            entity.HostId,
            entity.Name,
            entity.Location,
            entity.ImageUrl,
            entity.Description,
            entity.BasePrice,
            entity.Type,
            entity.Amenities
        );
    }
}