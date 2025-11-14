using System.Net.Mime;
using BackendAwSmartstay.API.Payments.Domain.Model.Commands;
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

    [HttpGet("{paymentId:int}")]
    [SwaggerOperation(
        Summary = "Get payment by id",
        Description = "Get payment by id",
        OperationId = "GetPaymentById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The payment", typeof(PaymentResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Payment not found")]
    public async Task<IActionResult> GetPaymentById(int paymentId)
    {
        var getPaymentByIdQuery = new GetPaymentByIdQuery(paymentId);
        var payment = await paymentQueryService.Handle(getPaymentByIdQuery);
        if (payment is null) return NotFound();
        var resource = PaymentResourceFromEntityAssembler.ToResourceFromEntity(payment);
        return Ok(resource);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new payment",
        Description = "Create a new payment",
        OperationId = "CreatePayment")]
    [SwaggerResponse(StatusCodes.Status201Created, "The payment was created", typeof(PaymentResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The payment could not be created")]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentResource resource)
    {
        var createPaymentCommand = CreatePaymentCommandFromResourceAssembler.ToCommandFromResource(resource);
        var payment = await paymentCommandService.Handle(createPaymentCommand);
        if (payment is null) return BadRequest();
        var paymentResource = PaymentResourceFromEntityAssembler.ToResourceFromEntity(payment);
        return CreatedAtAction(nameof(GetPaymentById), new { paymentId = payment.Id }, paymentResource);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all payments",
        Description = "Get all payments",
        OperationId = "GetAllPayments")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of payments", typeof(IEnumerable<PaymentResource>))]
    public async Task<IActionResult> GetAllPayments()
    {
        var payments = await paymentQueryService.Handle(new GetAllPaymentsQuery());
        var paymentResources = payments.Select(PaymentResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(paymentResources);
    }

    [HttpGet("booking/{bookingId:int}")]
    [SwaggerOperation(
        Summary = "Get payments by booking id",
        Description = "Get payments by booking id",
        OperationId = "GetPaymentsByBookingId")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of payments", typeof(IEnumerable<PaymentResource>))]
    public async Task<IActionResult> GetPaymentsByBookingId(int bookingId)
    {
        var payments = await paymentQueryService.Handle(new GetPaymentsByBookingIdQuery(bookingId));
        var paymentResources = payments.Select(PaymentResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(paymentResources);
    }
    
    [HttpPost("{paymentId:int}/process")]
    [SwaggerOperation(
        Summary = "Process a payment",
        Description = "Process a payment",
        OperationId = "ProcessPayment")]
    [SwaggerResponse(StatusCodes.Status200OK, "The payment was processed", typeof(PaymentResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Payment not found")]
    public async Task<IActionResult> ProcessPayment(int paymentId, [FromQuery] string? invoiceNumber = null)
    {
        var processPaymentCommand = new ProcessPaymentCommand(paymentId, invoiceNumber);
        var payment = await paymentCommandService.Handle(processPaymentCommand);
        if (payment is null) return NotFound();
        var paymentResource = PaymentResourceFromEntityAssembler.ToResourceFromEntity(payment);
        return Ok(paymentResource);
    }

    [HttpPost("{paymentId:int}/fail")]
    [SwaggerOperation(
        Summary = "Fail a payment",
        Description = "Fail a payment",
        OperationId = "FailPayment")]
    [SwaggerResponse(StatusCodes.Status200OK, "The payment was failed", typeof(PaymentResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Payment not found")]
    public async Task<IActionResult> FailPayment(int paymentId)
    {
        var failPaymentCommand = new FailPaymentCommand(paymentId);
        var payment = await paymentCommandService.Handle(failPaymentCommand);
        if (payment is null) return NotFound();
        var paymentResource = PaymentResourceFromEntityAssembler.ToResourceFromEntity(payment);
        return Ok(paymentResource);
    }
}

