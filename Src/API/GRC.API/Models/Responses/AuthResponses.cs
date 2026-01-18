namespace GRC.API.Models.Responses;

public class AuthResponse
{
    public string Message { get; set; } = string.Empty;
    public int? UserId { get; set; }
}

public class ErrorResponse
{
    public string Message { get; set; } = string.Empty;
}