using Dapper;
using Domain.Contracts.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Repositories;

public class LikeRepository : ILikeRepository
{
    private readonly NpgsqlConnection _connection;

    public LikeRepository(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();
    }

    public async Task CreateOrUpdateLikeByPostIdAsync(int postId, int userId)
    {
        const string query = @"
			CREATE OR REPLACE FUNCTION pg_temp.like_post(
				post_id_param INTEGER, 
				user_id_param INTEGER
			)
			RETURNS INTEGER AS $$
			DECLARE
				row_exists INTEGER;
			BEGIN
				PERFORM
				FROM likes
				WHERE post_id = post_id_param AND user_id = user_id_param;
					
				IF NOT FOUND THEN
					INSERT INTO likes (post_id, user_id)
					VALUES(post_id_param, user_id_param);
					RETURN 0;
				ELSE 
					DELETE FROM likes
					WHERE post_id= post_id_param AND user_id= user_id_param;
					RETURN 1;
				END IF;
			END;
			$$
			LANGUAGE plpgsql;

			SELECT pg_temp.like_post(@PostId, @UserId);	
        ";

        var parameters = new
        {
            PostId = postId,
            UserId = userId
        };

        await _connection.QueryAsync(query, parameters);
    }
}