using Application.Contracts.Documents.Requests.Post;
using Application.Contracts.Documents.Responses;
using Domain.Models;

namespace Application.Implementations.Mappers;

public static class PostMapper
{
    public static Post ToEntity(this CreatePostRequest createPostRequest, int ownerId)
    {
        return new Post(ownerId, createPostRequest.Image, createPostRequest.Message, createPostRequest.Privacy,
            DateTime.Now);
    }

    public static PostResponse ToResponse(Post post, PostOwnerResponse postOwnerResponse)
    {
        return new PostResponse
        {
            Id = post.Id,
            Owner = postOwnerResponse,
            Image = post.Image,
            Message = post.Message,
            Privacy = post.Privacy,
            CreatedAt = post.CreatedAt
        };
    }
}