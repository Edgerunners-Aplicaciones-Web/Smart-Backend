using BackendAwSmartstay.API.Accommodations.Application.Commands;
using BackendAwSmartstay.API.Accommodations.Domain.Entities;
using BackendAwSmartstay.API.Accommodations.Domain.Repositories;
using BackendAwSmartstay.API.shared.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BackendAwSmartstay.API.Accommodations.Interfaces.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccommodationsController : ControllerBase
{
    private readonly IAccommodationRepository _accommodationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AccommodationsController(
        IAccommodationRepository accommodationRepository,
        IUnitOfWork unitOfWork)
    {
        _accommodationRepository = accommodationRepository;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var accommodations = await _accommodationRepository.ListAsync();
        return Ok(accommodations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var accommodation = await _accommodationRepository.FindByIdAsync(id);
        if (accommodation == null)
            return NotFound();

        return Ok(accommodation);
    }

    [HttpGet("city/{city}")]
    public async Task<IActionResult> GetByCity(string city)
    {
        var accommodations = await _accommodationRepository.GetByCityAsync(city);
        return Ok(accommodations);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAccommodationCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

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

        return CreatedAtAction(nameof(GetById), new { id = accommodation.Id }, accommodation);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var accommodation = await _accommodationRepository.FindByIdAsync(id);
        if (accommodation == null)
            return NotFound();

        _accommodationRepository.Remove(accommodation);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}

