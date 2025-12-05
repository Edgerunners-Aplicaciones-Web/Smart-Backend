using BackendAwSmartstay.API.Accommodations.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Accommodations.Domain.Model.Commands;
using BackendAwSmartstay.API.Accommodations.Domain.Repositories;
using BackendAwSmartstay.API.Accommodations.Domain.Services;
using BackendAwSmartstay.API.Shared.Domain.Repositories;

namespace BackendAwSmartstay.API.Accommodations.Application.Internal.CommandServices;

/// <summary>
/// Handles command operations related to the Room aggregate.
/// Provides application-level logic for creating room entities.
/// </summary>
public class RoomCommandService(
    IRoomRepository roomRepository,
    IUnitOfWork unitOfWork)
    : IRoomCommandService
{
    public async Task<Room?> Handle(CreateRoomCommand command)
        /// <summary>
        /// Creates a new room based on the provided command
        /// and persists it through the repository.
        /// </summary>
        /// <param name="command">Command containing room creation data.</param>
        /// <returns>The created Room entity.</returns>
    {
        var room = new Room(command);
        await roomRepository.AddAsync(room);
        await unitOfWork.CompleteAsync();

        return room;
    }
}

