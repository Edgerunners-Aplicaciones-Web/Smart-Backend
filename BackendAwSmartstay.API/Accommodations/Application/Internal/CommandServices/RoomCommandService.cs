using BackendAwSmartstay.API.Accommodations.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Accommodations.Domain.Model.Commands;
using BackendAwSmartstay.API.Accommodations.Domain.Repositories;
using BackendAwSmartstay.API.Accommodations.Domain.Services;
using BackendAwSmartstay.API.Shared.Domain.Repositories;

namespace BackendAwSmartstay.API.Accommodations.Application.Internal.CommandServices;


public class RoomCommandService(
    IRoomRepository roomRepository,
    IUnitOfWork unitOfWork)
    : IRoomCommandService
{
    public async Task<Room?> Handle(CreateRoomCommand command)
    {
        var room = new Room(command);
        await roomRepository.AddAsync(room);
        await unitOfWork.CompleteAsync();

        return room;
    }
}

