using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Documents.Requests.Post;

public class CreatePostRequest
{
    public string Image { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição obrigatória.")]
    public string Message { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tipo de visualização obrigatório.")]
    public string Privacy { get; set; } = string.Empty;
}