namespace BackendAwSmartstay.API.Bookings.Interfaces.REST.Resources;

public record CreateBookingResource(int RoomId, string GuestName, string GuestEmail, DateTime CheckInDate, DateTime CheckOutDate);

