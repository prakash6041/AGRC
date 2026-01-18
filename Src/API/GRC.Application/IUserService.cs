using GRC.Domain.Entities;

namespace GRC.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task AssignRoleAsync(int userId, int roleId);
}