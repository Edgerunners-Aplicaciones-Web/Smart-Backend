using BackendAwSmartstay.API.Shared.Domain.Model.Events;

namespace BackendAwSmartstay.API.Accommodations.Domain.Entities;


public class Accommodation : IEvent
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int AccommodationTypeId { get; set; }
    public int? AccommodationSubTypeId { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual AccommodationType? AccommodationType { get; set; }
    public virtual AccommodationSubType? AccommodationSubType { get; set; }
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    public virtual ICollection<AccommodationAmenity> AccommodationAmenities { get; set; } = new List<AccommodationAmenity>();
}