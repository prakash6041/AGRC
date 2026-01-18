using GRC.Application.Interfaces;
using GRC.Domain.Entities;
using GRC.Domain.Interfaces;

namespace GRC.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task AssignRoleAsync(int userId, int roleId)
    {
        await _userRepository.AssignRoleAsync(userId, roleId);
    }
}