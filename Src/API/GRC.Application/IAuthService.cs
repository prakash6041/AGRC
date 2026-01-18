using GRC.Domain.Entities;

namespace GRC.Application.Interfaces;

public interface IAuthService
{
    Task<User?> RegisterAsync(string email, string password, int organizationId, string mobileNumber);
    Task<bool> LoginAsync(string email, string password);
}