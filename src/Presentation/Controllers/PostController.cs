using Application.Contracts;
using Application.Contracts.Documents.Requests.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpPost]
    [Authorize]
    [Route("/posts")]
    public async Task<IActionResult> CreatePost(CreatePostRequest createPostRequest)
    {
        var id = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type.Equals("Id"))?.Value!);

        var response = await _postService.CreateAsync(createPostRequest, id);

        return Created("Created", response);
    }

    [HttpGet]
    [Authorize]
    [Route("/users/{userId}/public-posts")]
    public async Task<IActionResult> GetPublicPosts([FromRoute] int userId) // TODO: docs
    {
        var response = await _postService.ListPublicUserPostsAsync(userId);

        return Ok(response);
    }
}