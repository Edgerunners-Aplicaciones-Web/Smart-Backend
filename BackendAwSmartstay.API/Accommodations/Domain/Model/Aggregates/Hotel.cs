using BackendAwSmartstay.API.Accommodations.Domain.Model.Commands;

namespace BackendAwSmartstay.API.Accommodations.Domain.Model.Aggregates;

public partial class Hotel
{
    public Hotel()
    {
        Name = string.Empty;
        Location = string.Empty;
        Description = string.Empty;
        ImageUrl = string.Empty;
        Type = string.Empty;
        Amenities = new List<string>();
    }

    public Hotel(CreateHotelCommand command) : this()
    {
        HostId = command.HostId;
        Name = command.Name;
        Location = command.Location;
        ImageUrl = command.ImageUrl;
        Description = command.Description;
        BasePrice = command.BasePrice;
        Type = command.Type;
        Amenities = command.Amenities;
    }

    public int Id { get; }
    public int HostId { get; private set; }
    public string Name { get; private set; }
    public string Location { get; private set; }
    public string ImageUrl { get; private set; }
    public string Description { get; private set; }
    public decimal BasePrice { get; private set; } // Usamos decimal para dinero
    public string Type { get; private set; }
    
    // EF Core 8+ maneja listas de primitivos nativamente en JSON o requiere ValueConverter
    public List<string> Amenities { get; private set; } 
}