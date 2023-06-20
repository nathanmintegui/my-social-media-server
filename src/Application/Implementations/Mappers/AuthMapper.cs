using Application.Contracts.Documents.Responses;
using Domain.Models;

namespace Application.Implementations.Mappers;

public static class AuthMapper
{
    public static LoginResponse ToResponse(this User user)
    {
        return new LoginResponse
        {
            IsAuthenticated = true,
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
}