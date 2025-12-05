using BackendAwSmartstay.API.Accommodations.Domain.Model.Commands;
using BackendAwSmartstay.API.Accommodations.Interfaces.REST.Resources;

namespace BackendAwSmartstay.API.Accommodations.Interfaces.REST.Transform;

/// <summary>
/// Assembler that converts a <see cref="CreateHotelResource"/> into a <see cref="CreateHotelCommand"/>.
/// This static helper centralizes the mapping from API resource to domain command.
/// </summary>
public static class CreateHotelCommandFromResourceAssembler
{
    /// <summary>
    /// Maps the provided resource to a domain command used to create a hotel.
    /// </summary>
    /// <param name="resource">The API resource containing hotel creation data.</param>
    /// <returns>
    /// A new <see cref="CreateHotelCommand"/> populated with values from the resource.
    /// </returns>
    public static CreateHotelCommand ToCommandFromResource(CreateHotelResource resource)
    {
        return new CreateHotelCommand(
            resource.HostId,
            resource.Name,
            resource.Address,
            resource.City,
            resource.Country,
            resource.ImageUrl,
            resource.Description,
            resource.Type,
            resource.Amenities
        );
    }
}