namespace Domain.Models;

public class Post
{
#pragma warning disable CS8618
    public Post()
#pragma warning restore CS8618
    {
    }

    public Post(int id, int ownerId, User owner, string image, string message, string privacy, List<Comment>? comments,
        DateTime createdAt)
    {
        Id = id;
        OwnerId = ownerId;
        Owner = owner;
        Image = image;
        Message = message;
        Privacy = privacy;
        Comments = comments;
        CreatedAt = createdAt;
    }

    public Post(int ownerId, string image, string message, string privacy, DateTime now)
    {
        OwnerId = ownerId;
        Image = image;
        Message = message;
        Privacy = privacy;
        CreatedAt = now;
    }

    public int Id { get; private set; }
    public int OwnerId { get; private set; }
    public User Owner { get; private set; }
    public string Image { get; private set; }
    public string Message { get; private set; }
    public string Privacy { get; private set; }
    public List<Comment?> Comments { get; set; } = new();
    public DateTime CreatedAt { get; private set; }
}