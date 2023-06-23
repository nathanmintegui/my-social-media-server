namespace Application.Contracts.Documents.Responses;

public class PostResponse
{
    public int Id { get; set; }
    public PostOwnerResponse Owner { get; set; }
    public string Image { get; set; }
    public string Message { get; set; }
    public string Privacy { get; set; }
    public DateTime CreatedAt { get; set; }
}