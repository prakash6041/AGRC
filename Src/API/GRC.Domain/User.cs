namespace GRC.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public int OrganizationId { get; set; }
    public int RoleId { get; set; }
    public bool Active { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? MobileNumber { get; set; }
}