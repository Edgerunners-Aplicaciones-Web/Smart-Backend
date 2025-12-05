using BackendAwSmartstay.API.Accommodations.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Accommodations.Interfaces.REST.Resources;

namespace BackendAwSmartstay.API.Accommodations.Interfaces.REST.Transform;

/// <summary>
/// Assembler that converts a <see cref="Hotel"/> domain entity into a <see cref="HotelResource"/>
/// suitable for REST responses. Encapsulates mapping logic such as building a display location
/// and computing the lowest price.
/// </summary>
public static class HotelResourceFromEntityAssembler
{
    /// <summary>
    /// Maps the provided <see cref="Hotel"/> entity to a <see cref="HotelResource"/>.
    /// The method builds a human-readable location string from the address, city and country,
    /// and obtains the lowest price using the entity's pricing logic.
    /// </summary>
    /// <param name="entity">The hotel domain entity to convert.</param>
    /// <returns>
    /// A new <see cref="HotelResource"/> populated with values derived from the entity,
    /// including the computed location display and lowest price.
    /// </returns>
    public static HotelResource ToResourceFromEntity(Hotel entity)
    {
        var locationDisplay = $"{entity.Address}, {entity.City}, {entity.Country}";

        var lowestPrice = entity.CalculateLowestPrice();

        return new HotelResource(
            entity.Id,
            entity.HostId,
            entity.Name,
            locationDisplay,
            entity.ImageUrl,
            entity.Description,
            lowestPrice,
            entity.Type,
            entity.Amenities
        );
    }
}