using Application.Implementations.Validations;
using static System.String;

namespace Application.Contracts.Documents.Responses;

public class LoginResponse : Notifiable
{
    public bool IsAuthenticated { get; set; } = false;
    public int Id { get; set; }
    public string Name { get; set; } = Empty;
    public string Email { get; set; } = Empty;
}