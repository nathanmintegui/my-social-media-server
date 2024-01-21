using Domain.Models;

namespace Domain.Contracts.Repositories;

/// <summary>
/// Repository Class that access Comment table with basic crud operations
/// </summary>
public interface ICommentRepository
{
    /// <summary>
    /// Create a comment on a post 
    /// </summary>
    /// <returns>The Comment Entity</returns>   
    public Task<Comment?> CreateAsync(string message, int authorId, int postId);
}