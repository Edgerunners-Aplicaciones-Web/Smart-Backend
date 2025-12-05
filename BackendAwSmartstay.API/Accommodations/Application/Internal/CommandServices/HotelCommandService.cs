using BackendAwSmartstay.API.Accommodations.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Accommodations.Domain.Model.Commands;
using BackendAwSmartstay.API.Accommodations.Domain.Repositories;
using BackendAwSmartstay.API.Accommodations.Domain.Services;
using BackendAwSmartstay.API.Shared.Domain.Repositories;

namespace BackendAwSmartstay.API.Accommodations.Application.Internal.CommandServices;

/// <summary>
///     Application command service responsible for handling write operations
///     This service follows the principles of Clean Architecture and Domain-Driven Design:
/// </summary>
public class HotelCommandService(
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork)
    : IHotelCommandService

{
    /// <summary>
    /// Creates a new hotel based on the provided command
    /// and persists it through the repository.
    /// </summary>
    /// <param name="command">Command containing hotel creation data.</param>
    /// <returns>The created Hotel entity.</returns>
    public async Task<Hotel?> Handle(CreateHotelCommand command)
    {
        var hotel = new Hotel(command);
        await hotelRepository.AddAsync(hotel);
        await unitOfWork.CompleteAsync();
        return hotel;
    }
}