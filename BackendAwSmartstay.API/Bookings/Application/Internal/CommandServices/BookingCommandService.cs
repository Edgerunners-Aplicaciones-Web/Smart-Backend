using BackendAwSmartstay.API.Bookings.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Bookings.Domain.Model.Commands;
using BackendAwSmartstay.API.Bookings.Domain.Repositories;
using BackendAwSmartstay.API.Bookings.Domain.Services;
using BackendAwSmartstay.API.Shared.Domain.Repositories;

namespace BackendAwSmartstay.API.Bookings.Application.Internal.CommandServices;


public class BookingCommandService(
    IBookingRepository bookingRepository,
    IUnitOfWork unitOfWork)
    : IBookingCommandService
{
    public async Task<Booking?> Handle(CreateBookingCommand command)
    {
        var booking = new Booking(command);
        await bookingRepository.AddAsync(booking);
        await unitOfWork.CompleteAsync();

        return booking;
    }

    public async Task<Booking?> Handle(ConfirmBookingCommand command)
    {
        var booking = await bookingRepository.FindByIdAsync(command.BookingId);
        if (booking is null) return null;

        booking.Confirm();
        bookingRepository.Update(booking);
        await unitOfWork.CompleteAsync();

        return booking;
    }

    public async Task<Booking?> Handle(CancelBookingCommand command)
    {
        var booking = await bookingRepository.FindByIdAsync(command.BookingId);
        if (booking is null) return null;

        booking.Cancel();
        bookingRepository.Update(booking);
        await unitOfWork.CompleteAsync();

        return booking;
    }
}

