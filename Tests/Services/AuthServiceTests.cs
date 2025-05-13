using AuthenticationApiProject.Application.Interfaces;
using AuthenticationApiProject.Application.Services;
using AuthenticationApiProject.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace AuthenticationApiProject.Tests.Services;


public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepo = new();
    private readonly Mock<IJwtService> _jwtService = new();
    private readonly AuthService _sut;

    public AuthServiceTests()
    {
        _sut = new AuthService(_userRepo.Object, _jwtService.Object);
    }

    [Fact]
    public async Task LoginAsync_WithValidCredentials_ReturnsToken()
    {
        var user = new User { Email = "test@test.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456") };
        _userRepo.Setup(r => r.GetByEmailAsync("test@test.com")).ReturnsAsync(user);
        _jwtService.Setup(j => j.GenerateToken(user.Username)).Returns("mock-token");

        var token = await _sut.LoginAsync("test@test.com", "123456");

        token.Should().Be("mock-token");
    }

    [Fact]
    public async Task LoginAsync_WithInvalidPassword_ReturnsNull()
    {
        var user = new User { Email = "test@test.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456") };
        _userRepo.Setup(r => r.GetByEmailAsync("test@test.com")).ReturnsAsync(user);

        var token = await _sut.LoginAsync("test@test.com", "wrongpassword");

        token.Should().BeNull();
    }
}