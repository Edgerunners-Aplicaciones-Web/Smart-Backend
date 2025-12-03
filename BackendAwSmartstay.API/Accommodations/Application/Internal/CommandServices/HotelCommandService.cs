using BackendAwSmartstay.API.Accommodations.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Accommodations.Domain.Model.Commands;
using BackendAwSmartstay.API.Accommodations.Domain.Repositories;
using BackendAwSmartstay.API.Accommodations.Domain.Services;
using BackendAwSmartstay.API.Shared.Domain.Repositories;

namespace BackendAwSmartstay.API.Accommodations.Application.Internal.CommandServices;

public class HotelCommandService(
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork)
    : IHotelCommandService
{
    public async Task<Hotel?> Handle(CreateHotelCommand command)
    {
        var hotel = new Hotel(command);
        await hotelRepository.AddAsync(hotel);
        await unitOfWork.CompleteAsync();
        return hotel;
    }
}