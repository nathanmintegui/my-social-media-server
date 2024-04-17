using Application.Contracts.Documents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
public class FriendshipController : ControllerBase
{
    private readonly IFriendshipService _friendshipService;

    public FriendshipController(IFriendshipService friendshipService)
    {
        _friendshipService = friendshipService;
    }

    [HttpPost]
    [Authorize]
    [Route("/friendships/{friendId}/invite")]
    public async Task<IActionResult> Invite([FromRoute] int friendId)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type.Equals("Id"))?.Value!);

        await _friendshipService.RequestFriendshipAsync(userId, friendId);

        return NoContent();
    }

    [HttpPatch]
    [Authorize]
    [Route("/friendships/invites/{inviteId}/accept")]
    public async Task<IActionResult> AcceptInvite([FromRoute] int inviteId)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type.Equals("Id"))?.Value!);

        await _friendshipService.AcceptFriendshipInviteAsync(userId, inviteId);

        return NoContent();
    }

    [HttpGet]
    [Authorize]
    [Route("/friendships")] // TODO: implement pagination 
    public async Task<IActionResult> ListFriends()
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type.Equals("Id"))?.Value!);

        var response = await _friendshipService.ListFriendsAsync(userId);

        return Ok(response);
    }
}