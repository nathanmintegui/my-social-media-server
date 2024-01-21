using System.ComponentModel.DataAnnotations;
using static System.String;

namespace Application.Contracts.Documents.Requests.Comment;

public class CreateCommentRequest
{
    public string Message { get; set; } = Empty;

    [Required(ErrorMessage = "Id do post é obrigatório.")]
    public int PostId { get; set; }
}