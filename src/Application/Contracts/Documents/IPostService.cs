using Application.Contracts.Documents.Requests.Post;
using Application.Contracts.Documents.Responses;

namespace Application.Contracts;

public interface IPostService
{
    Task<PostResponse> CreateAsync(CreatePostRequest createPostRequest, int id);
}