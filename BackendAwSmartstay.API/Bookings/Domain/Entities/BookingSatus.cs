namespace BackendAwSmartstay.API.Bookings.Domain.Entities;

/// <summary>
/// Estado de una reserva (Pendiente, Confirmada, Cancelada, Completada, etc.)
/// </summary>
public class BookingStatus
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}