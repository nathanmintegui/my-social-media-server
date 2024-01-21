using Application.Implementations.Validations;
using static System.String;

namespace Application.Contracts.Documents.Responses;

public class CommentResponse : Notifiable
{
    public int CommentId { get; set; }
    public string Message { get; set; } = Empty;
    public DateTime CreatedAt { get; set; }
    public int PostId { get; set; }
    public int AuthorId { get; set; }
}