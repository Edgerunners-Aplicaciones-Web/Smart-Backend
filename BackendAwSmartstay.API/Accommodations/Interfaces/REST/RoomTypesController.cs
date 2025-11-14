using System.Net.Mime;
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
[SwaggerTag("Available Room Type Endpoints")]
public class RoomTypesController(
    IRoomTypeCommandService roomTypeCommandService,
    IRoomTypeQueryService roomTypeQueryService) : ControllerBase
{

    [HttpGet("{roomTypeId:int}")]
    [SwaggerOperation(
        Summary = "Get room type by id",
        Description = "Get room type by id",
        OperationId = "GetRoomTypeById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The room type", typeof(RoomTypeResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Room type not found")]
    public async Task<IActionResult> GetRoomTypeById(int roomTypeId)
    {
        var getRoomTypeByIdQuery = new GetRoomTypeByIdQuery(roomTypeId);
        var roomType = await roomTypeQueryService.Handle(getRoomTypeByIdQuery);
        if (roomType is null) return NotFound();
        var resource = RoomTypeResourceFromEntityAssembler.ToResourceFromEntity(roomType);
        return Ok(resource);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new room type",
        Description = "Create a new room type",
        OperationId = "CreateRoomType")]
    [SwaggerResponse(StatusCodes.Status201Created, "The room type was created", typeof(RoomTypeResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The room type could not be created")]
    public async Task<IActionResult> CreateRoomType([FromBody] CreateRoomTypeResource resource)
    {
        var createRoomTypeCommand = CreateRoomTypeCommandFromResourceAssembler.ToCommandFromResource(resource);
        var roomType = await roomTypeCommandService.Handle(createRoomTypeCommand);
        if (roomType is null) return BadRequest();
        var roomTypeResource = RoomTypeResourceFromEntityAssembler.ToResourceFromEntity(roomType);
        return CreatedAtAction(nameof(GetRoomTypeById), new { roomTypeId = roomType.Id }, roomTypeResource);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all room types",
        Description = "Get all room types",
        OperationId = "GetAllRoomTypes")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of room types", typeof(IEnumerable<RoomTypeResource>))]
    public async Task<IActionResult> GetAllRoomTypes()
    {
        var roomTypes = await roomTypeQueryService.Handle(new GetAllRoomTypesQuery());
        var roomTypeResources = roomTypes.Select(RoomTypeResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(roomTypeResources);
    }
}

