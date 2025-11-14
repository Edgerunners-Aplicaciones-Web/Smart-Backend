using BackendAwSmartstay.API.Bookings.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Shared.Domain.Repositories;

namespace BackendAwSmartstay.API.Bookings.Domain.Repositories;

public interface IBookingRepository : IBaseRepository<Booking>
{
}

