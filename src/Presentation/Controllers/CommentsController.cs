using Application.Contracts;
using Application.Contracts.Documents.Requests.Comment;
using Application.Contracts.Documents.Responses;
using Application.Implementations.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Int32;

namespace Presentation.Controllers;

[ApiController]
[Route("comments")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CommentResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> createComment(CreateCommentRequest createCommentRequest)
    {
        var authorId = Parse(User.Claims.FirstOrDefault(claim => claim.Type.Equals("Id"))?.Value!);

        var response = await _commentService.CreateAsync(createCommentRequest, authorId);

        if (!response.IsValid()) return BadRequest(new ErrorResponse(response.Notifications));

        return Created("Created", response);
    }
}