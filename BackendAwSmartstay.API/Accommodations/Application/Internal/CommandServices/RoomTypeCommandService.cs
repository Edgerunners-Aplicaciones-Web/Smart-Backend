using BackendAwSmartstay.API.Accommodations.Domain.Model.Commands;
using BackendAwSmartstay.API.Accommodations.Domain.Model.Entities;
using BackendAwSmartstay.API.Accommodations.Domain.Repositories;
using BackendAwSmartstay.API.Accommodations.Domain.Services;
using BackendAwSmartstay.API.Shared.Domain.Repositories;

namespace BackendAwSmartstay.API.Accommodations.Application.Internal.CommandServices;


public class RoomTypeCommandService(
    IRoomTypeRepository roomTypeRepository,
    IUnitOfWork unitOfWork)
    : IRoomTypeCommandService
{
    /// <inheritdoc />
    public async Task<RoomType?> Handle(CreateRoomTypeCommand command)
    {
        var roomType = new RoomType(command);
        await roomTypeRepository.AddAsync(roomType);
        await unitOfWork.CompleteAsync();

        return roomType;
    }
}

