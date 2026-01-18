using GRC.Application.Interfaces;
using GRC.Domain.Entities;
using GRC.Domain.Interfaces;
using GRC.Application.Helpers;
using Microsoft.Extensions.Logging;

namespace GRC.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IUserRepository userRepository, ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<User?> RegisterAsync(string email, string password, int organizationId, string mobileNumber)
    {
        _logger.LogInformation("Registration attempt for email: {Email}", email);

        // Validate password strength
        var passwordValidation = PasswordHelper.ValidatePasswordStrength(password);
        if (!passwordValidation.IsValid)
        {
            _logger.LogWarning("Password validation failed for email {Email}: {Error}", email, passwordValidation.ErrorMessage);
            throw new ArgumentException(passwordValidation.ErrorMessage);
        }

        // Validate mobile number
        var mobileValidation = MobileNumberHelper.ValidateMobileNumber(mobileNumber);
        if (!mobileValidation.IsValid)
        {
            _logger.LogWarning("Mobile number validation failed for email {Email}: {Error}", email, mobileValidation.ErrorMessage);
            throw new ArgumentException(mobileValidation.ErrorMessage);
        }

        var existingUser = await _userRepository.GetByEmailAsync(email);
        if (existingUser != null)
        {
            _logger.LogWarning("Registration failed: User already exists for email {Email}", email);
            throw new Exception("User already exists");
        }

        var passwordHash = PasswordHelper.HashPassword(password);
        var normalizedMobile = MobileNumberHelper.NormalizeMobileNumber(mobileNumber);
        var user = new User
        {
            Email = email,
            PasswordHash = passwordHash,
            OrganizationId = organizationId,
            RoleId = 1, // Default role
            MobileNumber = normalizedMobile
        };

        var id = await _userRepository.AddAsync(user);
        user.Id = id;
        _logger.LogInformation("User registered successfully with ID {UserId} for email {Email}", id, email);
        return user;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null || !PasswordHelper.VerifyPassword(user.PasswordHash, password) || !user.Active)
            return false;

        return true;
    }

}