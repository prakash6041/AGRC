namespace GRC.Desktop.Blazor.Models
{
    public class UserSession
    {
        public string Token { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public RoleType Role { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}
