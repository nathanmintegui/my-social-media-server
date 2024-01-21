using Application.Contracts.Documents;
using Application.Contracts.Documents.Requests.Auth;
using Application.Contracts.Documents.Responses;
using Application.Implementations.Mappers;
using Application.Implementations.Validations;
using Application.Implementations.Validators;
using CrossCutting;
using Domain.Contracts.Repositories;

namespace Application.Implementations.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private const string MinimumAgeNotificationMessage = "Você precisa ter no mínimo 16 anos.";
    private const string UserNotAuthorizedNotificationMessage = "Usuário e/ou senha incorretos";
    private const string EmailAlreadyInUseNotificationMessage = "Email já cadastrado.";

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponse> SignUpAsync(SignUpRequest signUpRequest)
    {
        var response = new UserResponse();

        var isEmailAlreadyInUse = await _userRepository.ValidateEmail(signUpRequest.Email);
        if (!isEmailAlreadyInUse)
        {
            response.AddNotification(new Notification(EmailAlreadyInUseNotificationMessage));
            return response;
        }

        if (!signUpRequest.BirthDate.ValidateMinimumAge())
        {
            response.AddNotification(new Notification(MinimumAgeNotificationMessage));
            return response;
        }

        var hashedPassword = Utils.Hash(signUpRequest.Password);
        var entity = signUpRequest.ToEntity(hashedPassword);

        var user = await _userRepository.CreateUserAsync(entity);

        response = UserMapper.ToResponse(user!);

        return response;
    }

    public async Task<LoginResponse> ValidateLoginAsync(LoginRequest loginRequest)
    {
        var response = new LoginResponse();

        var hashedPassword = Utils.Hash(loginRequest.Password);

        var user = await _userRepository.GetUserAsync(loginRequest.Email, hashedPassword);

        if (user is null)
        {
            response.AddNotification(new Notification(UserNotAuthorizedNotificationMessage));
            return response;
        }

        response = AuthMapper.ToResponse(user);

        return response;
    }
}