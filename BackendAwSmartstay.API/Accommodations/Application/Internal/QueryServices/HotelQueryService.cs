using BackendAwSmartstay.API.Accommodations.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Accommodations.Domain.Model.Queries;
using BackendAwSmartstay.API.Accommodations.Domain.Repositories;
using BackendAwSmartstay.API.Accommodations.Domain.Services;

namespace BackendAwSmartstay.API.Accommodations.Application.Internal.QueryServices;

public class HotelQueryService(IHotelRepository hotelRepository) : IHotelQueryService
{
    public async Task<Hotel?> Handle(GetHotelByIdQuery query)
    {
        return await hotelRepository.FindByIdAsync(query.HotelId);
    }

    public async Task<IEnumerable<Hotel>> Handle(GetAllHotelsQuery query)
    {
        return await hotelRepository.ListAsync();
    }
}