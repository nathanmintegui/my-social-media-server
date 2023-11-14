using Dapper;
using Dapper.Contrib.Extensions;
using Domain.Contracts.Repositories;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly NpgsqlConnection _connection;

    public UserRepository(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();
    }

    public async Task<User?> GetUserAsync(string email, string password)
    {
        const string query = @"SELECT * from users
                               WHERE email=@Email AND password=@Password";

        var parameters = new
        {
            Email = email,
            Password = password
        };

        var user = await _connection.QueryFirstOrDefaultAsync<User>(query, parameters);

        return user;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        var user = await _connection.GetAsync<User>(id);

        return user;
    }

    public async Task<User?> CreateUserAsync(User user)
    {
        var userId = await _connection.InsertAsync(user);

        var response = await _connection.GetAsync<User>(userId);

        return response;
    }
}