using static System.String;

namespace Application.Contracts.Documents.Responses;

public class UserResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = Empty;
    public string Email { get; set; } = Empty;
    public string? Nickname { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Cep { get; set; }
    public string? Photo { get; set; }
}