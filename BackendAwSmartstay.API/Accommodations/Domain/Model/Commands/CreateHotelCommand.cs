namespace BackendAwSmartstay.API.Accommodations.Domain.Model.Commands;

public record CreateHotelCommand(
    int HostId,
    string Name,
    string Address,  
    string City,     
    string Country,  
    string ImageUrl,
    string Description,
    string Type,
    List<string> Amenities
);