using BackendAwSmartstay.API.Accommodations.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Accommodations.Domain.Model.Queries;
using BackendAwSmartstay.API.Accommodations.Domain.Repositories;
using BackendAwSmartstay.API.Accommodations.Domain.Services;

namespace BackendAwSmartstay.API.Accommodations.Application.Internal.QueryServices;


public class RoomQueryService(IRoomRepository roomRepository)
    : IRoomQueryService
{
    public async Task<Room?> Handle(GetRoomByIdQuery query)
    {
        return await roomRepository.FindByIdAsync(query.RoomId);
    }

    public async Task<IEnumerable<Room>> Handle(GetAllRoomsQuery query)
    {
        return await roomRepository.ListAsync();
    }

    public async Task<IEnumerable<Room>> Handle(GetRoomsByTypeQuery query)
    {
        var rooms = await roomRepository.ListAsync();
        return rooms.Where(r => r.RoomTypeId == query.RoomTypeId);
    }
}

