using BackendAwSmartstay.API.Bookings.Domain.Model.Commands;

namespace BackendAwSmartstay.API.Bookings.Domain.Model.Aggregates;


public partial class Booking
{

    public Booking()
    {
        GuestName = string.Empty;
        GuestEmail = string.Empty;
    }
    
    public Booking(int roomId, string guestName, string guestEmail, DateTime checkInDate, DateTime checkOutDate) : this()
    {
        RoomId = roomId;
        GuestName = guestName;
        GuestEmail = guestEmail;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        Status = BookingStatus.Pending;
    }
    
    public Booking(CreateBookingCommand command) : this(
        command.RoomId,
        command.GuestName,
        command.GuestEmail,
        command.CheckInDate,
        command.CheckOutDate)
    {
    }

    public int Id { get; }
    
    public int RoomId { get; private set; }

    public string GuestName { get; private set; }
    
    public string GuestEmail { get; private set; }

    public DateTime CheckInDate { get; private set; }

    public DateTime CheckOutDate { get; private set; }

    public BookingStatus Status { get; private set; }

    public void Confirm()
    {
        Status = BookingStatus.Confirmed;
    }

    public void Cancel()
    {
        Status = BookingStatus.Cancelled;
    }
}

public enum BookingStatus
{
    Pending = 0,
    Confirmed = 1,
    Cancelled = 2,
    Completed = 3
}

