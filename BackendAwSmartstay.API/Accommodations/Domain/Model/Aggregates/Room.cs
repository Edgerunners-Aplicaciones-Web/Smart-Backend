using BackendAwSmartstay.API.Accommodations.Domain.Model.Commands;
using BackendAwSmartstay.API.Accommodations.Domain.Model.Entities;

namespace BackendAwSmartstay.API.Accommodations.Domain.Model.Aggregates;
public partial class Room
{
    public Room()
    {
        Description = string.Empty;
        Amenities = new List<string>();
    }
    
    public Room(int roomTypeId, string description, List<string> amenities) : this()
    {
        RoomTypeId = roomTypeId;
        Description = description;
        Amenities = amenities;
    }
    public Room(CreateRoomCommand command) : this(command.RoomTypeId, command.Description, command.Amenities)
    {
    }
    
    public int Id { get; }
    
    public RoomType RoomType { get; internal set; }

    public int RoomTypeId { get; private set; }

    public string Description { get; private set; }

    public List<string> Amenities { get; private set; }
}

