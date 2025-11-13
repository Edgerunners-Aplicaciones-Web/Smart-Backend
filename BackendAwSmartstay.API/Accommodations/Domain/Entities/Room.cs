namespace BackendAwSmartstay.API.Accommodations.Domain.Entities;

/// <summary>
/// Representa una habitación dentro de un alojamiento
/// </summary>
public class Room
{
    public int Id { get; set; }
    public int AccommodationId { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public decimal PricePerNight { get; set; }
    public bool IsAvailable { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public virtual Accommodation? Accommodation { get; set; }
}