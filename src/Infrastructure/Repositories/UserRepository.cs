using Dapper;
using Domain.Contracts.Repositories;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using static CrossCutting.Utils;

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
        var hashedPassword = Hash(password);

        const string query = @"SELECT * from USERS
                               WHERE email = @email AND password = @password";

        var user = await _connection.QueryFirstAsync<User>(query, new { email, password });

        return user;
    }

    public async Task<User?> CreateUserAsync(User user)
    {
        const string query = @"INSERT INTO users(name, email, nickname, birth_date, cep, password, photo) 
        VALUES(@Name, @Email, @Nickname, @BirthDate, @Cep, @Password, @Photo) returning *";

        var parameters = new
        {
            Name = user.Name,
            Email = user.Email,
            Nickname = user.Nickname,
            Birthdate = user.BirthDate,
            Cep = user.Cep,
            Password = user.Password,
            Photo = user.Photo
        };

        var response = await _connection.QuerySingleAsync<User>(query, parameters);

        return response;
    }
}