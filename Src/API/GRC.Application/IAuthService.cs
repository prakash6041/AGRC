using GRC.Domain.Entities;

namespace GRC.Application.Interfaces;

public interface IAuthService
{
    Task<User?> RegisterAsync(string email, string password, string mobileNumber, string countryCode);
    Task<bool> LoginAsync(string email, string password);
}