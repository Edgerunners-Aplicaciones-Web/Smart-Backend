using BackendAwSmartstay.API.shared.Domain.Model.Events;

namespace BackendAwSmartstay.API.Bookings.Domain.Entities;

/// <summary>
/// Representa una reserva de alojamiento
/// </summary>
public class Booking : IEvent
{
    public int Id { get; set; }
    public int AccommodationId { get; set; }
    public int RoomId { get; set; }
    public int GuestId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int NumberOfGuests { get; set; }
    public decimal TotalAmount { get; set; }
    public int BookingStatusId { get; set; }
    public string? SpecialRequests { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public virtual BookingStatus? BookingStatus { get; set; }
}