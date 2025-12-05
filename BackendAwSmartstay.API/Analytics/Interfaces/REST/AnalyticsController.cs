using System.Net.Mime;
using BackendAwSmartstay.API.Analytics.Domain.Model.Queries;
using BackendAwSmartstay.API.Analytics.Domain.Services;
using BackendAwSmartstay.API.Analytics.Interfaces.REST.Resources;
using BackendAwSmartstay.API.Analytics.Interfaces.REST.Transform;
using BackendAwSmartstay.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BackendAwSmartstay.API.Analytics.Interfaces.REST;

/// <summary>
/// REST controller for analytics operations.
/// </summary>
[Authorize] // Only Staff/Admin should access this
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Analytics Endpoints")]
public class AnalyticsController(IAnalyticsQueryService analyticsQueryService) : ControllerBase
{
    /// <summary>
    /// Retrieves monthly performance metrics.
    /// </summary>
    /// <returns>An action result containing the performance metrics resource.</returns>
    [HttpGet("performance/monthly")]
    [SwaggerOperation(
        Summary = "Get monthly performance metrics",
        Description = "Retrieves aggregated metrics like revenue and occupancy for the current month.",
        OperationId = "GetMonthlyPerformance")]
    [SwaggerResponse(StatusCodes.Status200OK, "The metrics", typeof(PerformanceMetricsResource))]
    public async Task<IActionResult> GetMonthlyPerformance()
    {
        var query = new GetMonthlyPerformanceQuery();
        var metrics = await analyticsQueryService.Handle(query);
        var resource = PerformanceMetricsAssembler.ToResourceFromEntity(metrics);
        return Ok(resource);
    }
}