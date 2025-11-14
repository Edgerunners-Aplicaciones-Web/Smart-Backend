namespace BackendAwSmartstay.API.Accommodations.Interfaces.REST.Resources;


public record RoomResource(int Id, int RoomTypeId, string RoomTypeName, string Description, List<string> Amenities);

