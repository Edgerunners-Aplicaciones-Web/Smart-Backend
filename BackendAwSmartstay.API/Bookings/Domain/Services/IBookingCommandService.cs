using BackendAwSmartstay.API.Bookings.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Bookings.Domain.Model.Commands;

namespace BackendAwSmartstay.API.Bookings.Domain.Services;

public interface IBookingCommandService
{

    public Task<Booking?> Handle(CreateBookingCommand command);


    public Task<Booking?> Handle(ConfirmBookingCommand command);


    public Task<Booking?> Handle(CancelBookingCommand command);
}

