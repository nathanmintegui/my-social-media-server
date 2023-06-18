using static System.String;

namespace Application.Contracts.Documents.Responses;

public class LoginResponse
{
    public bool IsAuthenticated { get; set; } = false;
    public int Id { get; set; }
    public string Username { get; set; } = Empty;
    public string Email { get; set; } = Empty;
}