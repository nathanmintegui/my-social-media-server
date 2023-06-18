using Application.Contracts.Documents;
using Application.Contracts.Documents.Requests.Auth;
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
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest)
    {
        var response = await _authService.SignUpAsync(signUpRequest);

        return Created("sign-up", response);
    }

    [HttpPost]
    [Route("/login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _authService.ValidateLoginAsync(request);

        if (!response.IsAuthenticated) return BadRequest("Usuário e/ou senha incorretos.");

        var token = TokenService.GenerateToken(response);

        return Ok(token);
    }
}