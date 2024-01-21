using Application.Contracts.Documents.Requests.Comment;
using Application.Contracts.Documents.Responses;
using Domain.Models;

namespace Application.Contracts;

public interface ICommentService
{
    public Task<CommentResponse?> CreateAsync(CreateCommentRequest createCommentRequest, int authorId);
}