using Domain.Models;

namespace Domain.Contracts.Repositories;

public interface IPostRepository
{
    Task<Post?> CreatePostAsync(Post post, int ownerId);
}