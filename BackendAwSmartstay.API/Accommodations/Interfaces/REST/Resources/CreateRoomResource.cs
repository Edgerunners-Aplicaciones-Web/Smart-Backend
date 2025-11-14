namespace BackendAwSmartstay.API.Accommodations.Interfaces.REST.Resources;

public record CreateRoomResource(int RoomTypeId, string Description, List<string> Amenities);

