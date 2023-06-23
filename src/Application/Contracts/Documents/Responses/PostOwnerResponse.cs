namespace Application.Contracts.Documents.Responses;

public class PostOwnerResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
}