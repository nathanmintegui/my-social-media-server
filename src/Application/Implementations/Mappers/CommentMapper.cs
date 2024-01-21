using Application.Contracts.Documents.Responses;
using Domain.Models;

namespace Application.Implementations.Mappers;

public static class CommentMapper
{
    public static CommentResponse ToResponse(this Comment comment)
    {
        return new CommentResponse
        {
            CommentId = comment.CommentId,
            CreatedAt = comment.CreatedAt,
            Message = comment.Message,
            AuthorId = comment.AuthorId,
            PostId = comment.PostId
        };
    }
}