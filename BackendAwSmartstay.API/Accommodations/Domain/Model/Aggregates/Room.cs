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

    public Room(CreateRoomCommand command) : this()
    {
        RoomTypeId = command.RoomTypeId;
        // NUEVOS CAMPOS
        HotelId = command.HotelId;
        Price = command.Price;
        // -------------
        Description = command.Description;
        Amenities = command.Amenities;
    }
    
    /// <summary>
    /// Updates the mutable information of the room aggregate.
    /// This method enforces business invariants during updates.
    /// </summary>
    /// <param name="roomTypeId">The new room type identifier.</param>
    /// <param name="price">The new price per night.</param>
    /// <param name="description">The new description.</param>
    /// <param name="amenities">The new list of amenities.</param>
    public void UpdateInformation(int roomTypeId, decimal price, string description, List<string> amenities)
    {
        // Validation logic can be placed here (e.g., Price > 0)
        if (price < 0) 
            throw new ArgumentException("Price cannot be negative.");

        RoomTypeId = roomTypeId;
        Price = price;
        Description = description;
        Amenities = amenities;
    }

    public int Id { get; }
    public int RoomTypeId { get; private set; }
    
    // NUEVOS CAMPOS
    public int HotelId { get; private set; } // FK al Hotel
    public decimal Price { get; private set; } // Precio por noche
    // -------------
    
    public string Description { get; private set; }
    public List<string> Amenities { get; private set; }

    // Navigation Properties
    public virtual RoomType RoomType { get; private set; } 
    public virtual Hotel Hotel { get; private set; }
}