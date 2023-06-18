using Application.Contracts.Documents;
using Application.Contracts.Documents.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Security;

namespace Presentation.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;
    private readonly IAuthService _authService;

    public AuthController(TokenService tokenService, IAuthService authService)
    {
        _tokenService = tokenService;
        _authService = authService;
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