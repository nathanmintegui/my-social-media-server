using Dapper;
using Domain.Contracts.Repositories;
using Domain.Models;
using Npgsql;
using static CrossCutting.Utils;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private const string ConnectionString = "Host=localhost:5432;" +
                                            "Username=postgres;" +
                                            "Password=postgres;" +
                                            "Database=postgres";

    private readonly NpgsqlConnection _connection;

    public UserRepository()
    {
        _connection = new NpgsqlConnection(ConnectionString);
        _connection.Open();
    }

    public async Task<User?> GetUserAsync(string email, string password)
    {
        var hashedPassword = Hash(password);

        const string query = @"SELECT * from USERS
                               WHERE email = @email AND password = @password";

        var user = await _connection.QueryFirstAsync<User>(query, new { email, password });

        return user;
    }
}