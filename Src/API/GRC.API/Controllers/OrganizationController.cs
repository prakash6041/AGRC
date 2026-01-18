using Microsoft.AspNetCore.Mvc;
using GRC.Application.Interfaces;
using GRC.API.Models.Requests;
using GRC.API.Models.Responses;

namespace GRC.API.Controllers;

/// <summary>
/// Manages organization-related operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public OrganizationController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    /// <summary>
    /// Creates a new organization.
    /// </summary>
    /// <param name="request">The organization details.</param>
    /// <returns>A success response with organization ID.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationRequest request)
    {
        var id = await _organizationService.CreateOrganizationAsync(request.Name, request.Description);
        var response = new AuthResponse
        {
            Message = "Organization created successfully",
            UserId = id
        };
        return Ok(response);
    }

    /// <summary>
    /// Retrieves an organization by ID.
    /// </summary>
    /// <param name="id">The organization ID.</param>
    /// <returns>The organization details.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrganization(int id)
    {
        var organization = await _organizationService.GetOrganizationByIdAsync(id);
        if (organization == null)
            return NotFound();

        return Ok(organization);
    }
}