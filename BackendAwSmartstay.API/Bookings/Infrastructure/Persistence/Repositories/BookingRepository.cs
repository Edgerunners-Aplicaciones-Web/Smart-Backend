using BackendAwSmartstay.API.Bookings.Domain.Entities;
using BackendAwSmartstay.API.Bookings.Domain.Repositories;
using BackendAwSmartstay.API.shared.Infrastructure.Persistence.EFC.Configuration;
using BackendAwSmartstay.API.shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendAwSmartstay.API.Bookings.Infrastructure.Persistence.Repositories;

public class BookingRepository : BaseRepository<Booking>, IBookingRepository
{
    public BookingRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Booking>> GetByGuestIdAsync(int guestId)
    {
        return await Context.Set<Booking>()
            .Where(b => b.GuestId == guestId)
            .Include(b => b.BookingStatus)
            .ToListAsync();
    }

    public async Task<IEnumerable<Booking>> GetByAccommodationIdAsync(int accommodationId)
    {
        return await Context.Set<Booking>()
            .Where(b => b.AccommodationId == accommodationId)
            .Include(b => b.BookingStatus)
            .ToListAsync();
    }

    public async Task<IEnumerable<Booking>> GetByStatusIdAsync(int statusId)
    {
        return await Context.Set<Booking>()
            .Where(b => b.BookingStatusId == statusId)
            .Include(b => b.BookingStatus)
            .ToListAsync();
    }

    public async Task<IEnumerable<Booking>> GetBookingsByDateRangeAsync(DateTime checkIn, DateTime checkOut)
    {
        return await Context.Set<Booking>()
            .Where(b => 
                (b.CheckInDate <= checkOut && b.CheckOutDate >= checkIn))
            .Include(b => b.BookingStatus)
            .ToListAsync();
    }
}
