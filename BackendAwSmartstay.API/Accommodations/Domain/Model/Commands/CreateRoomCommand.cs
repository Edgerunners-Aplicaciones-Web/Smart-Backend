namespace BackendAwSmartstay.API.Accommodations.Domain.Model.Commands;

public record CreateRoomCommand(int RoomTypeId, string Description, List<string> Amenities);

