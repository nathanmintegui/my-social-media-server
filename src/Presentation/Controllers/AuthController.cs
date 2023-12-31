using Application.Contracts.Documents;
using Application.Contracts.Documents.Requests.Auth;
using Application.Contracts.Documents.Responses;
using Application.Implementations.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Security;

namespace Presentation.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService, IAuthService authService)
    {
        _tokenService = tokenService;
        _authService = authService;
    }

    [HttpPost]
    [Route("/sign-up")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest)
    {
        var response = await _authService.SignUpAsync(signUpRequest);

        if (!response.IsValid()) return BadRequest(new ErrorResponse(response.Notifications));

        return Created("sign-up", response);
    }

    [HttpPost]
    [Route("/login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _authService.ValidateLoginAsync(request);

        if (!response.IsValid()) return Unauthorized(new ErrorResponse(response.Notifications));

        var token = _tokenService.GenerateToken(response);

        return Ok(new {token = token});
    }
}