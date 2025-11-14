using BackendAwSmartstay.API.shared.Domain.Model.Events;

namespace BackendAwSmartstay.API.Bookings.Application.Commands;

public record CreateBookingCommand(
    int AccommodationId,
    int RoomId,
    int GuestId,
    DateTime CheckInDate,
    DateTime CheckOutDate,
    int NumberOfGuests,
    decimal TotalAmount,
    string? SpecialRequests
) : IEvent;