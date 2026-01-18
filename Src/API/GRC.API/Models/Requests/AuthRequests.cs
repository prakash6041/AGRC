using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GRC.API.Models.Requests;

public class RegisterRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(64, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 64 characters")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?])[A-Za-z\d!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{8,64}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    [JsonIgnore] // Don't serialize this field in API responses
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Organization ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid organization ID")]
    public int OrganizationId { get; set; }

    [Required(ErrorMessage = "Mobile number is required")]
    [RegularExpression(@"^\+?\d{7,15}$", ErrorMessage = "Invalid mobile number format")]
    public string MobileNumber { get; set; } = string.Empty;
}

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}