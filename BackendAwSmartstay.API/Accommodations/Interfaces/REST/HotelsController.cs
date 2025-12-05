using System.Net.Mime;
using BackendAwSmartstay.API.Accommodations.Domain.Model.Commands;
using BackendAwSmartstay.API.Accommodations.Domain.Model.Queries;
using BackendAwSmartstay.API.Accommodations.Domain.Services;
using BackendAwSmartstay.API.Accommodations.Interfaces.REST.Resources;
using BackendAwSmartstay.API.Accommodations.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BackendAwSmartstay.API.Accommodations.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Hotel Endpoints")]
public class HotelsController(
    IHotelCommandService hotelCommandService,
    IHotelQueryService hotelQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all hotels",
        Description = "Get all hotels",
        OperationId = "GetAllHotels")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of hotels", typeof(IEnumerable<HotelResource>))]
    public async Task<IActionResult> GetAllHotels()
    {
        var hotels = await hotelQueryService.Handle(new GetAllHotelsQuery());
        var resources = hotels.Select(HotelResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{hotelId:int}")]
    [SwaggerOperation(
        Summary = "Get hotel by id",
        Description = "Get hotel by id",
        OperationId = "GetHotelById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The hotel", typeof(HotelResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Hotel not found")]
    public async Task<IActionResult> GetHotelById(int hotelId)
    {
        var hotel = await hotelQueryService.Handle(new GetHotelByIdQuery(hotelId));
        if (hotel is null) return NotFound();
        var resource = HotelResourceFromEntityAssembler.ToResourceFromEntity(hotel);
        return Ok(resource);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new hotel",
        Description = "Create a new hotel",
        OperationId = "CreateHotel")]
    [SwaggerResponse(StatusCodes.Status201Created, "The hotel was created", typeof(HotelResource))]
    public async Task<IActionResult> CreateHotel([FromBody] CreateHotelResource resource)
    {
        var command = CreateHotelCommandFromResourceAssembler.ToCommandFromResource(resource);
        var hotel = await hotelCommandService.Handle(command);
        
        if (hotel is null) return BadRequest();
        
        var hotelResource = HotelResourceFromEntityAssembler.ToResourceFromEntity(hotel);
        return CreatedAtAction(nameof(GetHotelById), new { hotelId = hotel.Id }, hotelResource);
    }
    
    [HttpPut("{hotelId:int}")]
    [SwaggerOperation(
        Summary = "Update an existing hotel",
        Description = "Updates the details of a specific hotel.",
        OperationId = "UpdateHotel")]
    [SwaggerResponse(StatusCodes.Status200OK, "The hotel was updated", typeof(HotelResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Hotel not found")]
    public async Task<IActionResult> UpdateHotel(int hotelId, [FromBody] UpdateHotelResource resource)
    {
        var command = UpdateHotelCommandFromResourceAssembler.ToCommandFromResource(hotelId, resource);
        var updatedHotel = await hotelCommandService.Handle(command);

        if (updatedHotel is null) return NotFound();

        var hotelResource = HotelResourceFromEntityAssembler.ToResourceFromEntity(updatedHotel);
        return Ok(hotelResource);
    }

    [HttpDelete("{hotelId:int}")]
    [SwaggerOperation(
        Summary = "Delete a hotel",
        Description = "Deletes a specific hotel and its associated rooms.",
        OperationId = "DeleteHotel")]
    [SwaggerResponse(StatusCodes.Status200OK, "The hotel was deleted", typeof(HotelResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Hotel not found")]
    public async Task<IActionResult> DeleteHotel(int hotelId)
    {
        var command = new DeleteHotelCommand(hotelId);
        var deletedHotel = await hotelCommandService.Handle(command);

        if (deletedHotel is null) return NotFound();

        var hotelResource = HotelResourceFromEntityAssembler.ToResourceFromEntity(deletedHotel);
        return Ok(hotelResource);
    }
}