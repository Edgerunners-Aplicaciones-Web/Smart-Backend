namespace BackendAwSmartstay.API.Bookings.Interfaces.REST.Resources;

public record BookingResource(int Id, int RoomId, string GuestName, string GuestEmail, DateTime CheckInDate, DateTime CheckOutDate, string Status);

