using BackendAwSmartstay.API.Accommodations.Domain.Model.Commands;
using BackendAwSmartstay.API.Accommodations.Interfaces.REST.Resources;

namespace BackendAwSmartstay.API.Accommodations.Interfaces.REST.Transform;

public static class CreateHotelCommandFromResourceAssembler
{
    public static CreateHotelCommand ToCommandFromResource(CreateHotelResource resource)
    {
        return new CreateHotelCommand(
            resource.HostId,
            resource.Name,
            resource.Location,
            resource.ImageUrl,
            resource.Description,
            resource.BasePrice,
            resource.Type,
            resource.Amenities
        );
    }
}