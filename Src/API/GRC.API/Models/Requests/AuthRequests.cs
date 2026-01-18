using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GRC.API.Models.Requests;

/// <summary>
/// Request model for user registration with multi-national mobile support.
/// </summary>
public class RegisterRequest
{
    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    /// <remarks>Must be a valid email format (e.g., user@example.com)</remarks>
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's password.
    /// </summary>
    /// <remarks>
    /// Requirements:
    /// - Length: 8-64 characters
    /// - Must contain at least 1 uppercase letter (A-Z)
    /// - Must contain at least 1 lowercase letter (a-z)
    /// - Must contain at least 1 digit (0-9)
    /// - Must contain at least 1 special character (!@#$%^&*()_+-=[]{};\:'"|,.<>\/?)
    /// Example: Password@123
    /// </remarks>
    [Required(ErrorMessage = "Password is required")]
    [StringLength(64, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 64 characters")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?])[A-Za-z\d!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{8,64}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password confirmation.
    /// </summary>
    /// <remarks>Must match the Password field exactly. Not serialized in API responses.</remarks>
    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    [JsonIgnore] // Don't serialize this field in API responses
    public string ConfirmPassword { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's mobile number (without country code).
    /// </summary>
    /// <remarks>
    /// Format: Numeric digits only, 7-15 characters
    /// Examples: 
    /// - India: 9876543210 (10 digits)
    /// - USA: 2125551234 (10 digits)
    /// - UK: 2071838750 (10 digits)
    /// </remarks>
    [Required(ErrorMessage = "Mobile number is required")]
    [RegularExpression(@"^\d{7,15}$", ErrorMessage = "Invalid mobile number format")]
    public string MobileNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the country code for the mobile number.
    /// </summary>
    /// <remarks>
    /// Format: 1-3 digit numeric country code without the '+' sign.
    /// Common examples:
    /// - 91: India
    /// - 1: USA, Canada
    /// - 44: United Kingdom
    /// - 86: China
    /// - 81: Japan
    /// Call GET /api/auth/countries to get the complete list of supported country codes.
    /// </remarks>
    [Required(ErrorMessage = "Country code is required")]
    [RegularExpression(@"^\d{1,3}$", ErrorMessage = "Invalid country code")]
    public string CountryCode { get; set; } = string.Empty;
}

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}