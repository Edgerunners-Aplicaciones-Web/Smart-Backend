namespace BackendAwSmartstay.API.Accommodations.Domain.Entities;

/// <summary>
/// Relación entre Alojamiento y Comodidad (tabla de unión)
/// </summary>
public class AccommodationAmenity
{
    public int Id { get; set; }
    public int AccommodationId { get; set; }
    public int AmenityId { get; set; }
    public bool IsAvailable { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public virtual Accommodation? Accommodation { get; set; }
    public virtual Amenity? Amenity { get; set; }
}