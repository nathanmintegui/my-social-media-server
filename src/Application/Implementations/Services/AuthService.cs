using Application.Contracts.Documents;
using Application.Contracts.Documents.Requests.Auth;
using Application.Contracts.Documents.Responses;
using Application.Implementations.Mappers;
using CrossCutting;
using Domain.Contracts.Repositories;

namespace Application.Implementations.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponse> SignUpAsync(SignUpRequest signUpRequest)
    {
        var hashedPassword = Utils.Hash(signUpRequest.Password);
        var entity = signUpRequest.ToEntity(hashedPassword);

        var user = await _userRepository.CreateUserAsync(entity);

        if (user == null)
        {
            //TODO THROW NEW ERROR;
        }

        var response = user!.ToResponse();

        return response;
    }

    public async Task<LoginResponse> ValidateLoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetUserAsync(request.Email, request.Password);

        var response = new LoginResponse
            { IsAuthenticated = true, Email = user.Email, Id = user.Id, Username = user.Name };

        return response;
    }
}