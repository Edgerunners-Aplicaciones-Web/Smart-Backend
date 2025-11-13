namespace BackendAwSmartstay.API.Accommodations.Domain.Entities;

/// <summary>
/// Subtipo de alojamiento (Hotel 5 estrellas, Hostal económico, etc.)
/// </summary>
public class AccommodationSubType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int AccommodationTypeId { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public virtual AccommodationType? AccommodationType { get; set; }
    public virtual ICollection<Accommodation> Accommodations { get; set; } = new List<Accommodation>();
}