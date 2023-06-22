using Application.Contracts.Documents.Requests.Auth;
using Application.Contracts.Documents.Responses;

namespace Application.Contracts.Documents;

public interface IAuthService
{
    Task<UserResponse> SignUpAsync(SignUpRequest request);
    Task<LoginResponse> ValidateLoginAsync(LoginRequest request);
}