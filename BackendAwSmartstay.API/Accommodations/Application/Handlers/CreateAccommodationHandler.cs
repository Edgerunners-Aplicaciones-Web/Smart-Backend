using BackendAwSmartstay.API.Accommodations.Application.Commands;
using BackendAwSmartstay.API.Accommodations.Domain.Entities;
using BackendAwSmartstay.API.Accommodations.Domain.Repositories;
using BackendAwSmartstay.API.shared.Aplication.Internal.EventHandlers;
using BackendAwSmartstay.API.shared.Domain.Repositories;

namespace BackendAwSmartstay.API.Accommodations.Application.Handlers;

public class CreateAccommodationHandler : IEventHandler<CreateAccommodationCommand>
{
    private readonly IAccommodationRepository _accommodationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAccommodationHandler(
        IAccommodationRepository accommodationRepository,
        IUnitOfWork unitOfWork)
    {
        _accommodationRepository = accommodationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateAccommodationCommand command, CancellationToken cancellationToken)
    {
        var accommodation = new Accommodation
        {
            Name = command.Name,
            Description = command.Description,
            AccommodationTypeId = command.AccommodationTypeId,
            AccommodationSubTypeId = command.AccommodationSubTypeId,
            Address = command.Address,
            City = command.City,
            Country = command.Country,
            Latitude = command.Latitude,
            Longitude = command.Longitude,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _accommodationRepository.AddAsync(accommodation);
        await _unitOfWork.CompleteAsync();
    }
}