using Dapper;
using Domain.Contracts.Repositories;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly NpgsqlConnection _connection;

    public CommentRepository(IConfiguration configuration)
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();
    }

    public async Task<Comment?> CreateAsync(string message, int authorId, int postId)
    {
        const string query = @"INSERT INTO comment (message, created_at, post_id, author_id)
                                    VALUES(@Message, CURRENT_TIMESTAMP, @PostId, @AuthorId) returning *;";

        var parameters = new
        {
            Message = message,
            PostId = postId,
            AuthorId = authorId
        };

        var comment = await _connection.QueryFirstOrDefaultAsync<Comment>(query, parameters);

        await _connection.CloseAsync();

        return comment;
    }
}