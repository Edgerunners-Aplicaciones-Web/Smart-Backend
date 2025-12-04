using System.Net.Mime;
using BackendAwSmartstay.API.Payments.Domain.Model.Queries;
using BackendAwSmartstay.API.Payments.Domain.Services;
using BackendAwSmartstay.API.Payments.Interfaces.REST.Resources;
using BackendAwSmartstay.API.Payments.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BackendAwSmartstay.API.Payments.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Payment Endpoints")]
public class PaymentsController(
    IPaymentCommandService paymentCommandService,
    IPaymentQueryService paymentQueryService) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Process a new payment",
        Description = "Simulates a credit card payment for a booking.",
        OperationId = "ProcessPayment")]
    [SwaggerResponse(StatusCodes.Status201Created, "The payment was processed", typeof(PaymentResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid payment data")]
    public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentResource resource)
    {
        var command = ProcessPaymentCommandFromResourceAssembler.ToCommandFromResource(resource);
        var payment = await paymentCommandService.Handle(command);

        if (payment is null) return BadRequest("Could not process payment.");

        var paymentResource = PaymentResourceFromEntityAssembler.ToResourceFromEntity(payment);
        
        // Retornamos el recurso creado. Si falla la lógica de negocio (fondos), el status será Failed pero el recurso se crea.
        return CreatedAtAction(nameof(GetPaymentByBooking), new { bookingId = payment.BookingId }, paymentResource);
    }

    [HttpGet("booking/{bookingId:int}")]
    [SwaggerOperation(
        Summary = "Get payment by booking id",
        Description = "Retrieves the payment details associated with a booking.",
        OperationId = "GetPaymentByBooking")]
    [SwaggerResponse(StatusCodes.Status200OK, "The payment details", typeof(PaymentResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Payment not found")]
    public async Task<IActionResult> GetPaymentByBooking(int bookingId)
    {
        var query = new GetPaymentByBookingIdQuery(bookingId);
        var payment = await paymentQueryService.Handle(query);

        if (payment is null) return NotFound();

        var resource = PaymentResourceFromEntityAssembler.ToResourceFromEntity(payment);
        return Ok(resource);
    }
}