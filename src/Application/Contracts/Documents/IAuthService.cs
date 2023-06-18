using Application.Contracts.Documents.Requests;
using Application.Contracts.Documents.Responses;

namespace Application.Contracts.Documents;

public interface IAuthService
{
    Task<LoginResponse> ValidateLoginAsync(LoginRequest request);
}