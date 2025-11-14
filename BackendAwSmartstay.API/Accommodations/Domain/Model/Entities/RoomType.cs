using BackendAwSmartstay.API.Accommodations.Domain.Model.Commands;

namespace BackendAwSmartstay.API.Accommodations.Domain.Model.Entities;

public class RoomType
{

    public RoomType()
    {
        Name = string.Empty;
        Description = string.Empty;
    }
    
    public RoomType(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public RoomType(CreateRoomTypeCommand command)
    {
        Name = command.Name;
        Description = command.Description;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}

