using Domain.Models;

namespace Domain.Contracts.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserAsync(string email, string password);
}