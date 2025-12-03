namespace BackendAwSmartstay.API.Accommodations.Domain.Model.Commands;

public record CreateHotelCommand(
    int HostId,
    string Name,
    string Location,
    string ImageUrl,
    string Description,
    decimal BasePrice,
    string Type,
    List<string> Amenities
);