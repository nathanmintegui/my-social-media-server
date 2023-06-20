using System.ComponentModel.DataAnnotations;
using static System.String;

namespace Application.Contracts.Documents.Requests.Auth;

public class SignUpRequest
{
    [Required(ErrorMessage = "O campo nome é obrigatório.")]
    [MaxLength(255)]
    public string Name { get; set; } = Empty;

    [Required(ErrorMessage = "O campo email é obrigatório.")]
    [MaxLength(255)]
    [EmailAddress(ErrorMessage = "Este campo deve ter um endereço de email válido.")]
    public string Email { get; set; } = Empty;

    [MaxLength(255)] public string? Nickname { get; set; }

    [Required(ErrorMessage = "O campo data de nascimento é obrigatório.")]
    public DateTime BirthDate { get; set; }

    [Required(ErrorMessage = "O campo CEP é obrigatório.")]
    [MaxLength(8)]
    public string Cep { get; set; } = Empty;

    [Required(ErrorMessage = "O campo senha é obrigatório.")]
    [MaxLength(255)]
    public string Password { get; set; } = Empty;

    [MaxLength(512)] public string? Photo { get; set; }
}