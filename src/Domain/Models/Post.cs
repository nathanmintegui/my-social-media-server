namespace Domain.Models;

public class Post
{
#pragma warning disable CS8618
    public Post()
#pragma warning restore CS8618
    {
    }

    public Post(int ownerId, string image, string message, string privacy, DateTime createdAt)
    {
        OwnerId = ownerId;
        Image = image;
        Message = message;
        Privacy = privacy;
        CreatedAt = createdAt;
    }

    public int Id { get; private set; }
    public int OwnerId { get; private set; }
    public User Owner { get; private set; }
    public string Image { get; private set; }
    public string Message { get; private set; }
    public string Privacy { get; private set; }
    public DateTime CreatedAt { get; private set; }
}