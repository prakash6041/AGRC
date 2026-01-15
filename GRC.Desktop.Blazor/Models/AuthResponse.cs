namespace GRC.Desktop.Blazor.Models
{
    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Token { get; set; }
        public bool RequiresOTP { get; set; }
        public string? SessionId { get; set; }
    }
}
