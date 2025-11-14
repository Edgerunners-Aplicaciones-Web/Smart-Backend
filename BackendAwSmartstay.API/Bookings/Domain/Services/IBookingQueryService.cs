using BackendAwSmartstay.API.Bookings.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Bookings.Domain.Model.Queries;

namespace BackendAwSmartstay.API.Bookings.Domain.Services;
public interface IBookingQueryService
{
    Task<Booking?> Handle(GetBookingByIdQuery query);
    Task<IEnumerable<Booking>> Handle(GetAllBookingsQuery query);
    Task<IEnumerable<Booking>> Handle(GetBookingsByRoomIdQuery query);
}

