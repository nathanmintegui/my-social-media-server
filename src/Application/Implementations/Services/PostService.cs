using Application.Contracts;
using Application.Contracts.Documents.Requests.Post;
using Application.Contracts.Documents.Responses;
using Application.Implementations.Mappers;
using Domain.Contracts.Repositories;
using Domain.Models;

namespace Application.Implementations.Services;

public class PostService : IPostService
{
    private readonly ILikeRepository _likeRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public PostService(IPostRepository postRepository, IUserRepository userRepository, ILikeRepository likeRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _likeRepository = likeRepository;
    }

    public async Task<PostResponse> CreateAsync(CreatePostRequest createPostRequest, int id)
    {
        var entity = createPostRequest.ToEntity(id);

        var post = await _postRepository.CreatePostAsync(entity, id);

        var postOwner = await _userRepository.GetUserByIdAsync(id);
        var postOwnerResponse = new PostOwnerResponse
            { Id = postOwner!.Id, Name = postOwner.Name, Nickname = postOwner.Nickname!, Photo = postOwner.Photo! };

        var response = PostMapper.ToResponse(post!, postOwnerResponse);

        return response!;
    }

    public async Task<List<Post>?> ListPublicUserPostsAsync(int userId)
    {
        _ = await _userRepository.GetUserByIdAsync(userId) ??
            throw new Exception($"Usuário com ID {userId} não encontrado.");

        var response = await _postRepository.GetPublicUserPostsByIdAsync(userId);

        return response;
    }

    public async Task LikePostAsync(int postId, int userId) // TODO: user validatitor and post validator
    {
        _ = await _userRepository.GetUserByIdAsync(userId) ??
            throw new Exception($"Usuário com ID {userId} não encontrado.");

        _ = await _postRepository.GetPostByIdAsync(postId) ??
            throw new Exception($"Post com ID {postId} não encontrado.");

        // TODO: make shure i am friend of the post owner when its private

        await _likeRepository.CreateOrUpdateLikeByPostIdAsync(postId, userId);
    }
}