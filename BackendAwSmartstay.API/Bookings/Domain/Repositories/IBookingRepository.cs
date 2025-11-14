using BackendAwSmartstay.API.Bookings.Domain.Entities;
using BackendAwSmartstay.API.shared.Domain.Repositories;

namespace BackendAwSmartstay.API.Bookings.Domain.Repositories;

public interface IBookingRepository : IBaseRepository<Booking>
{
    Task<IEnumerable<Booking>> GetByGuestIdAsync(int guestId);
    Task<IEnumerable<Booking>> GetByAccommodationIdAsync(int accommodationId);
    Task<IEnumerable<Booking>> GetByStatusIdAsync(int statusId);
    Task<IEnumerable<Booking>> GetBookingsByDateRangeAsync(DateTime checkIn, DateTime checkOut);
}