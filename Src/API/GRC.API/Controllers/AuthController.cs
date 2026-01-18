using Microsoft.AspNetCore.Mvc;
using GRC.Application.Interfaces;
using GRC.API.Models.Requests;
using GRC.API.Models.Responses;
using Microsoft.Extensions.Logging;

namespace GRC.API.Controllers;

/// <summary>
/// Handles authentication operations including registration, login, and OTP verification.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Registers a new user with email, password, and mobile number.
    /// </summary>
    /// <param name="request">The registration details including:
    /// - Email: Valid email address
    /// - Password: 8-64 characters, must contain 1 uppercase, 1 lowercase, 1 digit, 1 special character
    /// - ConfirmPassword: Must match the Password field
    /// - MobileNumber: 7-15 digits (without country code)
    /// - CountryCode: 1-3 digit country code (e.g., 91 for India, 1 for USA, 44 for UK)
    /// </param>
    /// <returns>A success response with user ID if registration succeeds.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        // Model validation
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            var errorResponse = new ErrorResponse { Message = "Validation failed: " + string.Join(", ", errors) };
            return BadRequest(errorResponse);
        }

        // Additional password validation (confirm password matching)
        if (request.Password != request.ConfirmPassword)
        {
            var errorResponse = new ErrorResponse { Message = "Invalid registration data provided" };
            return BadRequest(errorResponse);
        }

        // Validate mobile number and country code
        if (string.IsNullOrWhiteSpace(request.MobileNumber) || string.IsNullOrWhiteSpace(request.CountryCode))
        {
            var errorResponse = new ErrorResponse { Message = "Invalid registration data provided" };
            return BadRequest(errorResponse);
        }

        try
        {
            _logger.LogInformation("Processing registration request for email: {Email}", request.Email);
            var user = await _authService.RegisterAsync(request.Email, request.Password, request.MobileNumber, request.CountryCode);
            var response = new AuthResponse
            {
                Message = "User registered successfully",
                UserId = user?.Id
            };
            _logger.LogInformation("Registration successful for email: {Email}", request.Email);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Registration validation error for email {Email}: {Error}", request.Email, ex.Message);
            var errorResponse = new ErrorResponse { Message = "Invalid registration data provided" };
            return BadRequest(errorResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Registration failed for email {Email}", request.Email);
            var errorResponse = new ErrorResponse { Message = "Registration failed. Please try again." };
            return BadRequest(errorResponse);
        }
    }

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="request">The login credentials.</param>
    /// <returns>A response indicating login success.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var isValid = await _authService.LoginAsync(request.Email, request.Password);
        if (!isValid)
        {
            var errorResponse = new ErrorResponse { Message = "Invalid credentials" };
            return Unauthorized(errorResponse);
        }

        var response = new AuthResponse
        {
            Message = "Login successful"
        };
        return Ok(response);
    }

    /// <summary>
    /// Gets all supported countries with their country codes.
    /// </summary>
    /// <returns>A list of supported countries.</returns>
    [HttpGet("countries")]
    public IActionResult GetSupportedCountries()
    {
        var countries = GRC.Application.Helpers.MobileNumberHelper.GetSupportedCountries();
        var result = countries.Select(x => new { code = x.Key, name = x.Value }).ToList();
        return Ok(result);
    }
}