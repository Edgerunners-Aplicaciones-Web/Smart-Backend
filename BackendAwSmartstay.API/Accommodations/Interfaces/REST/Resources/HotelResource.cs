namespace BackendAwSmartstay.API.Accommodations.Interfaces.REST.Resources;

public record HotelResource(
    int Id,
    int HostId,
    string Name,
    string Location,
    string ImageUrl,
    string Description,
    decimal BasePrice,
    string Type,
    List<string> Amenities
);