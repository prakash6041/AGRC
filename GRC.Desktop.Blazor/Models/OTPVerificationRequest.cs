namespace GRC.Desktop.Blazor.Models
{
    public class OTPVerificationRequest
    {
        public string SessionId { get; set; } = string.Empty;
        public string OTPCode { get; set; } = string.Empty;
    }
}
