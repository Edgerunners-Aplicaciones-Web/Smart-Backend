namespace BackendAwSmartstay.API.Accommodations.Domain.Entities;

/// <summary>
/// Tipo de alojamiento (Hotel, Hostal, Cabaña, etc.)
/// </summary>
public class AccommodationType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public virtual ICollection<Accommodation> Accommodations { get; set; } = new List<Accommodation>();
    public virtual ICollection<AccommodationSubType> AccommodationSubTypes { get; set; } = new List<AccommodationSubType>();
}