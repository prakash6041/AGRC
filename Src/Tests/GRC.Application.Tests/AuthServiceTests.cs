using Xunit;
using FakeItEasy;
using GRC.Application.Services;
using GRC.Domain.Entities;
using GRC.Domain.Interfaces;

namespace GRC.Application.Tests;

public class AuthServiceTests
{
    [Fact]
    public async Task RegisterAsync_ShouldReturnUser_WhenUserDoesNotExist()
    {
        // Arrange
        var userRepository = A.Fake<IUserRepository>();
        A.CallTo(() => userRepository.GetByEmailAsync("test@example.com")).Returns((User?)null);
        A.CallTo(() => userRepository.AddAsync(A<User>.Ignored)).Returns(1);

        var authService = new AuthService(userRepository);

        // Act
        var result = await authService.RegisterAsync("test@example.com", "password", "John", "Doe", 1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("test@example.com", result.Email);
    }

    [Fact]
    public async Task RegisterAsync_ShouldThrowException_WhenUserExists()
    {
        // Arrange
        var userRepository = A.Fake<IUserRepository>();
        A.CallTo(() => userRepository.GetByEmailAsync("test@example.com")).Returns(new User { Email = "test@example.com" });

        var authService = new AuthService(userRepository);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => authService.RegisterAsync("test@example.com", "password", "John", "Doe", 1));
    }
}