using System.Net.Mime;
using BackendAwSmartstay.API.Bookings.Domain.Model.Commands;
using BackendAwSmartstay.API.Bookings.Domain.Model.Queries;
using BackendAwSmartstay.API.Bookings.Domain.Services;
using BackendAwSmartstay.API.Bookings.Interfaces.REST.Resources;
using BackendAwSmartstay.API.Bookings.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BackendAwSmartstay.API.Bookings.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Booking Endpoints")]
public class BookingsController(
    IBookingCommandService bookingCommandService,
    IBookingQueryService bookingQueryService) : ControllerBase
{

    [HttpGet("{bookingId:int}")]
    [SwaggerOperation(
        Summary = "Get booking by id",
        Description = "Get booking by id",
        OperationId = "GetBookingById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The booking", typeof(BookingResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Booking not found")]
    public async Task<IActionResult> GetBookingById(int bookingId)
    {
        var getBookingByIdQuery = new GetBookingByIdQuery(bookingId);
        var booking = await bookingQueryService.Handle(getBookingByIdQuery);
        if (booking is null) return NotFound();
        var resource = BookingResourceFromEntityAssembler.ToResourceFromEntity(booking);
        return Ok(resource);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new booking",
        Description = "Create a new booking",
        OperationId = "CreateBooking")]
    [SwaggerResponse(StatusCodes.Status201Created, "The booking was created", typeof(BookingResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The booking could not be created")]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingResource resource)
    {
        var createBookingCommand = CreateBookingCommandFromResourceAssembler.ToCommandFromResource(resource);
        var booking = await bookingCommandService.Handle(createBookingCommand);
        if (booking is null) return BadRequest();
        var bookingResource = BookingResourceFromEntityAssembler.ToResourceFromEntity(booking);
        return CreatedAtAction(nameof(GetBookingById), new { bookingId = booking.Id }, bookingResource);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all bookings",
        Description = "Get all bookings",
        OperationId = "GetAllBookings")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of bookings", typeof(IEnumerable<BookingResource>))]
    public async Task<IActionResult> GetAllBookings()
    {
        var bookings = await bookingQueryService.Handle(new GetAllBookingsQuery());
        var bookingResources = bookings.Select(BookingResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(bookingResources);
    }

    [HttpGet("room/{roomId:int}")]
    [SwaggerOperation(
        Summary = "Get bookings by room id",
        Description = "Get bookings by room id",
        OperationId = "GetBookingsByRoomId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of bookings", typeof(IEnumerable<BookingResource>))]
    public async Task<IActionResult> GetBookingsByRoomId(int roomId)
    {
        var bookings = await bookingQueryService.Handle(new GetBookingsByRoomIdQuery(roomId));
        var bookingResources = bookings.Select(BookingResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(bookingResources);
    }
    
    [HttpPost("{bookingId:int}/confirm")]
    [SwaggerOperation(
        Summary = "Confirm a booking",
        Description = "Confirm a booking",
        OperationId = "ConfirmBooking")]
    [SwaggerResponse(StatusCodes.Status200OK, "The booking was confirmed", typeof(BookingResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Booking not found")]
    public async Task<IActionResult> ConfirmBooking(int bookingId)
    {
        var confirmBookingCommand = new ConfirmBookingCommand(bookingId);
        var booking = await bookingCommandService.Handle(confirmBookingCommand);
        if (booking is null) return NotFound();
        var bookingResource = BookingResourceFromEntityAssembler.ToResourceFromEntity(booking);
        return Ok(bookingResource);
    }


    [HttpPost("{bookingId:int}/cancel")]
    [SwaggerOperation(
        Summary = "Cancel a booking",
        Description = "Cancel a booking",
        OperationId = "CancelBooking")]
    [SwaggerResponse(StatusCodes.Status200OK, "The booking was cancelled", typeof(BookingResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Booking not found")]
    public async Task<IActionResult> CancelBooking(int bookingId)
    {
        var cancelBookingCommand = new CancelBookingCommand(bookingId);
        var booking = await bookingCommandService.Handle(cancelBookingCommand);
        if (booking is null) return NotFound();
        var bookingResource = BookingResourceFromEntityAssembler.ToResourceFromEntity(booking);
        return Ok(bookingResource);
    }
}

