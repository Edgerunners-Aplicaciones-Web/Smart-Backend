using System.Net.Mime;
using BackendAwSmartstay.API.Accommodations.Domain.Model.Commands;
using BackendAwSmartstay.API.Accommodations.Domain.Model.Queries;
using BackendAwSmartstay.API.Accommodations.Domain.Services;
using BackendAwSmartstay.API.Accommodations.Interfaces.REST.Resources;
using BackendAwSmartstay.API.Accommodations.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace BackendAwSmartstay.API.Accommodations.Interfaces.REST;


[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Room Endpoints")]
public class RoomsController(
    IRoomCommandService roomCommandService,
    IRoomQueryService roomQueryService) : ControllerBase
{

    [HttpGet("{roomId:int}")]
    [SwaggerOperation(
        Summary = "Get room by id",
        Description = "Get room by id",
        OperationId = "GetRoomById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The room", typeof(RoomResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Room not found")]
    public async Task<IActionResult> GetRoomById(int roomId)
    {
        var getRoomByIdQuery = new GetRoomByIdQuery(roomId);
        var room = await roomQueryService.Handle(getRoomByIdQuery);
        if (room is null) return NotFound();
        var resource = RoomResourceFromEntityAssembler.ToResourceFromEntity(room);
        return Ok(resource);
    }


    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new room",
        Description = "Create a new room",
        OperationId = "CreateRoom")]
    [SwaggerResponse(StatusCodes.Status201Created, "The room was created", typeof(RoomResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The room could not be created")]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomResource resource)
    {
        var createRoomCommand = CreateRoomCommandFromResourceAssembler.ToCommandFromResource(resource);
        var room = await roomCommandService.Handle(createRoomCommand);
        if (room is null) return BadRequest();
        var roomResource = RoomResourceFromEntityAssembler.ToResourceFromEntity(room);
        return CreatedAtAction(nameof(GetRoomById), new { roomId = room.Id }, roomResource);
    }


    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all rooms",
        Description = "Get all rooms",
        OperationId = "GetAllRooms")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of rooms", typeof(IEnumerable<RoomResource>))]
    public async Task<IActionResult> GetAllRooms()
    {
        var rooms = await roomQueryService.Handle(new GetAllRoomsQuery());
        var roomResources = rooms.Select(RoomResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(roomResources);
    }
    
    [HttpGet("type/{roomTypeId:int}")]
    [SwaggerOperation(
        Summary = "Get rooms by type",
        Description = "Get rooms by type",
        OperationId = "GetRoomsByType")]
    [SwaggerResponse(StatusCodes.Status200OK, "The list of rooms", typeof(IEnumerable<RoomResource>))]
    public async Task<IActionResult> GetRoomsByType(int roomTypeId)
    {
        var rooms = await roomQueryService.Handle(new GetRoomsByTypeQuery(roomTypeId));
        var roomResources = rooms.Select(RoomResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(roomResources);
    }
    
    [HttpPut("{roomId:int}")]
    [SwaggerOperation(
        Summary = "Update an existing room",
        Description = "Updates the details of a specific room.",
        OperationId = "UpdateRoom")]
    [SwaggerResponse(StatusCodes.Status200OK, "The room was updated", typeof(RoomResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Room not found")]
    public async Task<IActionResult> UpdateRoom(int roomId, [FromBody] UpdateRoomResource resource)
    {
        var command = UpdateRoomCommandFromResourceAssembler.ToCommandFromResource(roomId, resource);
        var updatedRoom = await roomCommandService.Handle(command);

        if (updatedRoom is null) return NotFound();

        var roomResource = RoomResourceFromEntityAssembler.ToResourceFromEntity(updatedRoom);
        return Ok(roomResource);
    }

    [HttpDelete("{roomId:int}")]
    [SwaggerOperation(
        Summary = "Delete a room",
        Description = "Deletes a specific room.",
        OperationId = "DeleteRoom")]
    [SwaggerResponse(StatusCodes.Status200OK, "The room was deleted", typeof(RoomResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Room not found")]
    public async Task<IActionResult> DeleteRoom(int roomId)
    {
        var command = new DeleteRoomCommand(roomId);
        var deletedRoom = await roomCommandService.Handle(command);

        if (deletedRoom is null) return NotFound();

        var roomResource = RoomResourceFromEntityAssembler.ToResourceFromEntity(deletedRoom);
        return Ok(roomResource);
    }
}

