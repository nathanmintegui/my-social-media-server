namespace Domain.Models;

public class Friendship
{
    public int FriendshipId { get; private set; }
    public Situation Status { get; private set; }
    public int RequesterId { get; private set; }
    public int AccepterId { get; private set; }
    public DateTime CreatedAt { get; private set; } 
}