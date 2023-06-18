using static System.String;

namespace Application.Contracts.Documents.Requests.Auth;

public class SignUpRequest
{
    public string Name { get; set; } = Empty;
    public string Email { get; set; } = Empty;
    public string? Nickname { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Cep { get; set; }
    public string Password { get; set; } = Empty;
    public string? Photo { get; set; }
}