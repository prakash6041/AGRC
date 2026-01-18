using Microsoft.AspNetCore.Mvc;
using GRC.Application.Interfaces;
using GRC.API.Models.Requests;
using GRC.API.Models.Responses;

namespace GRC.API.Controllers;

/// <summary>
/// Manages user-related operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Retrieves all users.
    /// </summary>
    /// <returns>A list of users.</returns>
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    /// <summary>
    /// Assigns a role to a user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="request">The role assignment details.</param>
    /// <returns>A success response.</returns>
    [HttpPost("{userId}/assign-role")]
    public async Task<IActionResult> AssignRole(int userId, [FromBody] AssignRoleRequest request)
    {
        await _userService.AssignRoleAsync(userId, request.RoleId);
        var response = new AuthResponse { Message = "Role assigned successfully" };
        return Ok(response);
    }
}