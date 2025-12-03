using BackendAwSmartstay.API.Accommodations.Domain.Model.Aggregates;
using BackendAwSmartstay.API.Accommodations.Domain.Model.Commands;

namespace BackendAwSmartstay.API.Accommodations.Domain.Services;

public interface IHotelCommandService
{
    Task<Hotel?> Handle(CreateHotelCommand command);
}