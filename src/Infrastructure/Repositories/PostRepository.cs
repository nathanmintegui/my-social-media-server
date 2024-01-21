using Dapper;
using Domain.Contracts.Repositories;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly NpgsqlConnection _connection;

    public PostRepository(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();
    }

    public async Task<Post?> CreatePostAsync(Post post, int ownerId)
    {
        const string query = @"INSERT INTO post (ownerid, image, message, privacy, created_at)
        VALUES (@OwnerId, @Image, @Message, @Privacy, @CreatedAt) returning *";

        var parameters = new
        {
            OwnerId = ownerId,
            Image = post.Image,
            Message = post.Message,
            Privacy = post.Privacy,
            CreatedAt = post.CreatedAt
        };

        var response = await _connection.QueryFirstOrDefaultAsync<Post>(query, parameters);

        return response;
    }

    public async Task<bool> ValidateIfPostExistsAsync(int postId)
    {
        const string query = @"SELECT 1
                                FROM post
                                WHERE id = @PostId";

        var parameters = new
        {
            PostId = postId
        };

        var response = await _connection.QueryFirstOrDefaultAsync<bool>(query, parameters);

        return response;
    }
}