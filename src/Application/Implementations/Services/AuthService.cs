using Application.Contracts.Documents;
using Application.Contracts.Documents.Requests;
using Application.Contracts.Documents.Responses;
using Domain.Contracts.Repositories;

namespace Application.Implementations.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<LoginResponse> ValidateLoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetUserAsync(request.Email, request.Password);

        var response = new LoginResponse
            { IsAuthenticated = true, Email = user.Email, Id = user.Id, Username = user.Name };

        return response;
    }
}