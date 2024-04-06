using Application.Contracts.Documents.Requests.Post;
using Application.Contracts.Documents.Responses;
using Domain.Models;

namespace Application.Contracts;

public interface IPostService
{
    Task<PostResponse> CreateAsync(CreatePostRequest createPostRequest, int id);
    Task<List<Post>?> ListPublicUserPostsAsync(int userId);
}