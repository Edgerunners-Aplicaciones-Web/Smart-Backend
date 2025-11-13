using BackendAwSmartstay.API.Shared.Domain.Model.Events;

namespace BackendAwSmartstay.API.Accommodations.Application.Commands;

public record CreateAccommodationCommand(
    string Name,
    string Description,
    int AccommodationTypeId,
    int? AccommodationSubTypeId,
    string Address,
    string City,
    string Country,
    decimal? Latitude,
    decimal? Longitude
) : IEvent;