using Microsoft.AspNetCore.Mvc;

namespace GRC.API.Controllers;

/// <summary>
/// Provides health check endpoints for the API.
/// </summary>
[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    /// <summary>
    /// Returns the health status of the API.
    /// </summary>
    /// <returns>A health status response.</returns>
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { Status = "Healthy", Timestamp = DateTime.UtcNow });
    }
}