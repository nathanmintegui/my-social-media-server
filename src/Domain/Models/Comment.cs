using System.ComponentModel.DataAnnotations;
using static System.String;

namespace Domain.Models;

public class Comment
{
    [Key] public int CommentId { get; private set; }
    public string Message { get; private set; } = Empty;
    public DateTime CreatedAt { get; private set; }
    public int PostId { get; private set; }
    public int AuthorId { get; private set; }
}