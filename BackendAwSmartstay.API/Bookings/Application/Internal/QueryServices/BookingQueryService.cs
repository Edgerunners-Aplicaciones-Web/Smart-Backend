using BackendAwSmartstay.API.Bookings.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Bookings.Domain.Model.Queries;
using BackendAwSmartstay.API.Bookings.Domain.Repositories;
using BackendAwSmartstay.API.Bookings.Domain.Services;

namespace BackendAwSmartstay.API.Bookings.Application.Internal.QueryServices;

public class BookingQueryService(IBookingRepository bookingRepository)
    : IBookingQueryService
{
    public async Task<Booking?> Handle(GetBookingByIdQuery query)
    {
        return await bookingRepository.FindByIdAsync(query.BookingId);
    }

    public async Task<IEnumerable<Booking>> Handle(GetAllBookingsQuery query)
    {
        return await bookingRepository.ListAsync();
    }

    public async Task<IEnumerable<Booking>> Handle(GetBookingsByRoomIdQuery query)
    {
        var bookings = await bookingRepository.ListAsync();
        return bookings.Where(b => b.RoomId == query.RoomId);
    }
}

