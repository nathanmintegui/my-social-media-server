using Application.Contracts;
using Application.Contracts.Documents.Requests.Comment;
using Application.Contracts.Documents.Responses;
using Application.Implementations.Mappers;
using Application.Implementations.Validations;
using Domain.Contracts.Repositories;
using static System.String;

namespace Application.Implementations.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private const string PostDoesntExistNotificationMessage = "Este post não existe.";
    private const string MessageIsEmptyNotificationMessage = "O campo mensagem é obrigatório";

    public CommentService(ICommentRepository commentRepository, IPostRepository postRepository)
    {
        _commentRepository = commentRepository;
        _postRepository = postRepository;
    }

    public async Task<CommentResponse?> CreateAsync(CreateCommentRequest createCommentRequest, int authorId)
    {
        var response = new CommentResponse();

        if (IsNullOrEmpty(createCommentRequest.Message))
        {
            response.AddNotification(new Notification(MessageIsEmptyNotificationMessage));
            return response;
        }

        var isPostIdValid = await _postRepository.ValidateIfPostExistsAsync(createCommentRequest.PostId);
        if (!isPostIdValid)
        {
            response.AddNotification(new Notification(PostDoesntExistNotificationMessage));
            return response;
        }

        var comment = await _commentRepository.CreateAsync(createCommentRequest.Message, authorId,
            createCommentRequest.PostId);

        response = comment!.ToResponse();

        return response;
    }
}