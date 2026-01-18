using System.Text.RegularExpressions;

namespace GRC.Desktop.Blazor.Helpers
{
    public static class PasswordValidationHelper
    {
        private static readonly string[] CommonWeakPasswords = new[]
        {
            "password", "123456", "123456789", "qwerty", "abc123", "password123",
            "admin", "letmein", "welcome", "monkey", "1234567890", "password1",
            "qwerty123", "admin123", "root", "user", "guest", "test", "demo"
        };

        /// <summary>
        /// Validates password strength according to security requirements
        /// </summary>
        public static (bool IsValid, string ErrorMessage) ValidatePasswordStrength(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return (false, "Password is required");

            if (password.Length < 8)
                return (false, "Password must be at least 8 characters long");

            if (password.Length > 64)
                return (false, "Password must not exceed 64 characters");

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return (false, "Password must contain at least one uppercase letter");

            if (!Regex.IsMatch(password, @"[a-z]"))
                return (false, "Password must contain at least one lowercase letter");

            if (!Regex.IsMatch(password, @"[0-9]"))
                return (false, "Password must contain at least one digit");

            if (!Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]"))
                return (false, "Password must contain at least one special character");

            if (CommonWeakPasswords.Contains(password.ToLowerInvariant()))
                return (false, "Password is too common. Please choose a stronger password");

            return (true, string.Empty);
        }

        /// <summary>
        /// Validates that confirm password matches the original password
        /// </summary>
        public static (bool IsValid, string ErrorMessage) ValidateConfirmPassword(string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(confirmPassword))
                return (false, "Please confirm your password");

            if (password != confirmPassword)
                return (false, "Passwords do not match");

            return (true, string.Empty);
        }
    }
}