using static System.String;

namespace Application.Contracts.Documents.Requests.Auth;

public class LoginRequest
{
    public string Email { get; set; } = Empty;
    public string Password { get; set; } = Empty;
}