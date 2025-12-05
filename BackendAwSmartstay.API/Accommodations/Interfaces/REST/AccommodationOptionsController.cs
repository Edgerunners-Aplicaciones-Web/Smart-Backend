using BackendAwSmartstay.API.Accommodations.Interfaces.REST.Resources;
using BackendAwSmartstay.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace BackendAwSmartstay.API.Accommodations.Interfaces.REST;

[ApiController]
[Route("api/v1/accommodations/options")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Accommodation Options (Master Data)")]
public class AccommodationOptionsController(AppDbContext context) : ControllerBase
{
    // --- GET Methods ---

    [HttpGet("categories")]
    [SwaggerOperation(Summary = "Get hotel categories")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await context.Set<Domain.Model.Entities.HotelCategory>().ToListAsync();
        return Ok(categories.Select(c => c.Name));
    }

    [HttpGet("amenities")]
    [SwaggerOperation(Summary = "Get available amenities")]
    public async Task<IActionResult> GetAmenities()
    {
        var amenities = await context.Set<Domain.Model.Entities.Amenity>().ToListAsync();
        return Ok(amenities.Select(a => a.Name));
    }

    // --- POST Methods ---

    [HttpPost("categories")]
    [SwaggerOperation(Summary = "Create a new hotel category")]
    [SwaggerResponse(StatusCodes.Status201Created, "The category was created")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryResource resource)
    {
        var exists = await context.Set<Domain.Model.Entities.HotelCategory>()
            .AnyAsync(c => c.Name == resource.Name);
            
        if (exists) return Conflict($"Category '{resource.Name}' already exists.");
        
        var category = new Domain.Model.Entities.HotelCategory { Name = resource.Name };
        
        context.Set<Domain.Model.Entities.HotelCategory>().Add(category);
        await context.SaveChangesAsync();

        // Usamos CreatedAtAction apuntando al GET de lista, ya que no tenemos GET by ID
        return CreatedAtAction(nameof(GetCategories), new { }, category.Name);
    }

    [HttpPost("amenities")]
    [SwaggerOperation(Summary = "Create a new amenity")]
    [SwaggerResponse(StatusCodes.Status201Created, "The amenity was created")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "The amenity already exists")]
    public async Task<IActionResult> CreateAmenity([FromBody] CreateAmenityResource resource)
    {
        var exists = await context.Set<Domain.Model.Entities.Amenity>()
            .AnyAsync(a => a.Name == resource.Name);
            
        if (exists) return Conflict($"Amenity '{resource.Name}' already exists.");

        var amenity = new Domain.Model.Entities.Amenity { Name = resource.Name };
        
        context.Set<Domain.Model.Entities.Amenity>().Add(amenity);
        await context.SaveChangesAsync();

        // Usamos CreatedAtAction apuntando al GET de lista
        return CreatedAtAction(nameof(GetAmenities), new { }, amenity.Name);
    }
}