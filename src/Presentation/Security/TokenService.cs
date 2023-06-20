using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Contracts.Documents.Responses;
using Microsoft.IdentityModel.Tokens;
using static System.DateTime;
using static System.Security.Claims.ClaimTypes;
using static System.Text.Encoding;
using static Microsoft.IdentityModel.Tokens.SecurityAlgorithms;
using static Presentation.Security.TokenSettings;

namespace Presentation.Security;

public class TokenService
{
    public string GenerateToken(LoginResponse login)
    {
        var secretKey = UTF8.GetBytes(SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = UtcNow.AddMinutes(ExpiresInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey),
                HmacSha256Signature),
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", login.Id.ToString()),
                new Claim(Name, login.Name),
                new Claim(Email, login.Email)
            })
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }
}