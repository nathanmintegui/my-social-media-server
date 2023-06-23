namespace Domain.Models;

public class PostOwner
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Nickname { get; set; }
    public string? Photo { get; set; }
}