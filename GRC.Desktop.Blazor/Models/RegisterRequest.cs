namespace GRC.Desktop.Blazor.Models
{
    public class RegisterRequest
    {
        public OrganizationType OrganizationType { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public RoleType RoleType { get; set; }
        public string MobileNumber { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
    }
}
