using Domain.Models;

namespace Domain.Contracts.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserAsync(string email, string password);
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> CreateUserAsync(User user);
    Task<bool> ValidateEmail(string email);
}