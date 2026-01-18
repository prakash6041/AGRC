using System.Text.RegularExpressions;

namespace GRC.Application.Helpers;

public static class MobileNumberHelper
{
    private static readonly Regex ValidMobileRegex = new Regex(@"^\+?\d{7,15}$", RegexOptions.Compiled);

    public static (bool IsValid, string? ErrorMessage) ValidateMobileNumber(string? mobileNumber)
    {
        if (string.IsNullOrWhiteSpace(mobileNumber))
        {
            return (false, "Mobile number is required");
        }

        // Check format: digits only with optional leading '+'
        if (!ValidMobileRegex.IsMatch(mobileNumber))
        {
            return (false, "Invalid mobile number format");
        }

        // Ensure length between 7 and 15 digits (excluding +)
        var digitsOnly = mobileNumber.TrimStart('+');
        if (digitsOnly.Length < 7 || digitsOnly.Length > 15)
        {
            return (false, "Mobile number must be between 7 and 15 digits");
        }

        return (true, null);
    }

    public static string? NormalizeMobileNumber(string? mobileNumber)
    {
        if (string.IsNullOrWhiteSpace(mobileNumber))
        {
            return null;
        }

        var digitsOnly = Regex.Replace(mobileNumber, @"[^\d]", "");
        if (digitsOnly.Length < 7 || digitsOnly.Length > 15)
        {
            return null; // Invalid length
        }

        // Assume default country code if not provided
        if (!mobileNumber.StartsWith("+"))
        {
            // For simplicity, prepend +1 (USA), but in production, use proper country detection
            return $"+1{digitsOnly}";
        }

        return $"+{digitsOnly}";
    }
}