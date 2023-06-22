using Application.Contracts.Documents;
using Application.Contracts.Documents.Requests.Auth;
using Application.Contracts.Documents.Responses;
using Application.Implementations.Services;
using Domain.Contracts.Repositories;
using Domain.Models;
using Moq;

namespace ApplicationTest.Implementations.Services;

public class AuthServiceTests
{
    private readonly IAuthService _authService;
    private readonly Mock<IUserRepository> _userRepository = new();

    public AuthServiceTests()
    {
        _authService = new AuthService(_userRepository.Object);
    }

    [Fact]
    public async Task SignUpAsync_ShouldReturnUser_WhenRequestIsValid()
    {
        // Arrange
        var birthDate = DateTime.Now.AddYears(-20);
        var signUpRequest = new SignUpRequest
        {
            Name = "maikito",
            Email = "maikito@example.com",
            Nickname = "maikito",
            BirthDate = birthDate,
            Cep = "0800",
            Password = "123",
            Photo = ""
        };

        var expectedUser = new User("maikito", "maikito@example.com", "maikito",
            birthDate, "0800", "123", "");

        _userRepository.Setup(repository => repository.CreateUserAsync(It.IsAny<User>()))
            .ReturnsAsync(expectedUser);

        // Act
        var result = await _authService.SignUpAsync(signUpRequest);

        // Assert
        Assert.IsType<UserResponse>(result);
        Assert.Equal(signUpRequest.Name, result.Name);
        Assert.Equal(signUpRequest.Email, result.Email);
        Assert.Equal(signUpRequest.Nickname, result.Nickname);
        Assert.Equal(signUpRequest.BirthDate, result.BirthDate);
        Assert.Equal(signUpRequest.Cep, result.Cep);
        Assert.Equal(signUpRequest.Photo, result.Photo);
    }
    
    [Fact]
    public async Task SignUpAsync_ShouldReturnErrorResponse_WhenUserIsUnderage()
    {
        // Arrange
        var birthDate = DateTime.Now.AddYears(-15);
        var signUpRequest = new SignUpRequest
        {
            Name = "maikito",
            Email = "maikito@example.com",
            Nickname = "maikito",
            BirthDate = birthDate,
            Cep = "0800",
            Password = "123",
            Photo = ""
        };
        
        var expectedUser = new User("maikito", "maikito@example.com", "maikito",
            birthDate, "0800", "123", "");

        _userRepository.Setup(repository => repository.CreateUserAsync(It.IsAny<User>()))
            .ReturnsAsync(expectedUser);
        
        // Act
        var result = await _authService.SignUpAsync(signUpRequest);

        // Assert
        Assert.IsType<UserResponse>(result);
        Assert.Single(result.Notifications);
        Assert.Equal("Você precisa ter no mínimo 16 anos.", result.Notifications.ElementAt(0).Message);
    }
}
