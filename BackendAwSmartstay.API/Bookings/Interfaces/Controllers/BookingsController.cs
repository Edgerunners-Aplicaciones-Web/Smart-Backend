using BackendAwSmartstay.API.Bookings.Application.Commands;
using BackendAwSmartstay.API.Bookings.Domain.Entities;
using BackendAwSmartstay.API.Bookings.Domain.Repositories;
using BackendAwSmartstay.API.shared.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackendAwSmartstay.API.Bookings.Interfaces.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BookingsController(
        IBookingRepository bookingRepository,
        IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var bookings = await _bookingRepository.ListAsync();
        return Ok(bookings);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var booking = await _bookingRepository.FindByIdAsync(id);
        if (booking == null)
            return NotFound();

        return Ok(booking);
    }

    [HttpGet("guest/{guestId}")]
    public async Task<IActionResult> GetByGuestId(int guestId)
    {
        var bookings = await _bookingRepository.GetByGuestIdAsync(guestId);
        return Ok(bookings);
    }

    [HttpGet("accommodation/{accommodationId}")]
    public async Task<IActionResult> GetByAccommodationId(int accommodationId)
    {
        var bookings = await _bookingRepository.GetByAccommodationIdAsync(accommodationId);
        return Ok(bookings);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookingCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

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

        return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var booking = await _bookingRepository.FindByIdAsync(id);
        if (booking == null)
            return NotFound();

        _bookingRepository.Remove(booking);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}
