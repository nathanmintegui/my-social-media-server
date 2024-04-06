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
        DefaultTypeMap.MatchNamesWithUnderscores = true;

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

    public async Task<List<Post>?> GetPublicUserPostsByIdAsync(int userId) //TODO: try catch
    {
        const string query = @"
            SELECT 
                p.*,
                c.*
            FROM 
                post p
            LEFT JOIN
                comment c 
                ON c.post_id = p.id 
            WHERE 
                owner_id = @UserId
            AND p.privacy = 'PUBLIC';
        ";

        var parameters = new
        {
            UserId = userId
        };

        var postDictionary = new Dictionary<int, Post>();

        await _connection.QueryAsync<Post, Comment, Post>(query, (post, comment) =>
        {
            if (!postDictionary.TryGetValue(post.Id, out var postEntry))
            {
                postEntry = post;
                postEntry.Comments = new List<Comment?>();
                postDictionary.Add(postEntry.Id, postEntry);
            }

            if (comment != null)
                postEntry.Comments.Add(comment);

            return postEntry;
        }, splitOn: "comment_id", param: parameters);

        return postDictionary.Values.ToList();
    }
}