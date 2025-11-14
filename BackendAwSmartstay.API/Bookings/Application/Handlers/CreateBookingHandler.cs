using BackendAwSmartstay.API.Bookings.Application.Commands;
using BackendAwSmartstay.API.Bookings.Domain.Entities;
using BackendAwSmartstay.API.Bookings.Domain.Repositories;
using BackendAwSmartstay.API.shared.Aplication.Internal.EventHandlers;
using BackendAwSmartstay.API.shared.Domain.Repositories;

namespace BackendAwSmartstay.API.Bookings.Application.Handlers;

public class CreateBookingHandler : IEventHandler<CreateBookingCommand>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBookingHandler(
        IBookingRepository bookingRepository,
        IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateBookingCommand command, CancellationToken cancellationToken)
    {
        var booking = new Booking
        {
            AccommodationId = command.AccommodationId,
            RoomId = command.RoomId,
            GuestId = command.GuestId,
            CheckInDate = command.CheckInDate,
            CheckOutDate = command.CheckOutDate,
            NumberOfGuests = command.NumberOfGuests,
            TotalAmount = command.TotalAmount,
            SpecialRequests = command.SpecialRequests,
            BookingStatusId = 1, // Pendiente por defecto
            CreatedAt = DateTime.UtcNow
        };

        await _bookingRepository.AddAsync(booking);
        await _unitOfWork.CompleteAsync();
    }
}