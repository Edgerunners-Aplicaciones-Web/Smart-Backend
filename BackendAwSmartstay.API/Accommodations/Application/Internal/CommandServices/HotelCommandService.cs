using BackendAwSmartstay.API.Accommodations.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Accommodations.Domain.Model.Commands;
using BackendAwSmartstay.API.Accommodations.Domain.Repositories;
using BackendAwSmartstay.API.Accommodations.Domain.Services;
using BackendAwSmartstay.API.Shared.Domain.Repositories;

namespace BackendAwSmartstay.API.Accommodations.Application.Internal.CommandServices;

/// <summary>
/// Service implementation for handling hotel commands.
/// Orchestrates the flow between the repository and the domain logic.
/// </summary>
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

    public async Task<Hotel?> Handle(UpdateHotelCommand command)
    {
        var hotel = await hotelRepository.FindByIdAsync(command.Id);
        if (hotel is null) return null;

        // Apply domain logic update
        hotel.UpdateInformation(
            command.Name, 
            command.Address, 
            command.City, 
            command.Country, 
            command.ImageUrl, 
            command.Description, 
            command.Type, 
            command.Amenities);

        hotelRepository.Update(hotel);
        await unitOfWork.CompleteAsync();
        return hotel;
    }

    public async Task<Hotel?> Handle(DeleteHotelCommand command)
    {
        var hotel = await hotelRepository.FindByIdAsync(command.Id);
        if (hotel is null) return null;

        hotelRepository.Remove(hotel);
        await unitOfWork.CompleteAsync();
        return hotel;
    }
}