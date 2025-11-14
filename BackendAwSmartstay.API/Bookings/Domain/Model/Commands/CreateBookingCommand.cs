namespace BackendAwSmartstay.API.Bookings.Domain.Model.Commands;


public record CreateBookingCommand(int RoomId, string GuestName, string GuestEmail, DateTime CheckInDate, DateTime CheckOutDate);

