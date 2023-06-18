using Application.Contracts.Documents.Requests.Auth;
using Application.Contracts.Documents.Responses;
using Domain.Models;

namespace Application.Implementations.Mappers;

public static class UserMapper
{
    public static User ToEntity(this SignUpRequest signUpRequest, string hashedPassword)
    {
        return new User
        (
            signUpRequest.Name,
            signUpRequest.Email,
            signUpRequest.Nickname!,
            signUpRequest.BirthDate,
            signUpRequest.Cep!,
            hashedPassword,
            signUpRequest.Photo!
        );
    }

    public static UserResponse ToResponse(this User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Nickname = user.Nickname,
            BirthDate = user.BirthDate,
            Cep = user.Cep,
            Photo = user.Photo
        };
    }
}