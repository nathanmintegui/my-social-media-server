using Application.Contracts;
using Application.Contracts.Documents.Requests.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("/posts")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreatePost(CreatePostRequest createPostRequest)
    {
        var id = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type.Equals("Id"))?.Value!);

        var response = await _postService.CreateAsync(createPostRequest, id);

        return Created("Created", response);
    }
}