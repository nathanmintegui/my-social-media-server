using Dapper;
using Domain.Contracts.Repositories;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Repositories;

public class FriendshipRepository : IFriendshipRepository
{
    private readonly NpgsqlConnection _connection;

    public FriendshipRepository(IConfiguration configuration)
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();
    }

    public async Task<int?> GetFriendshipSituationAsync(int userId, int friendId)
    {
        const string query = @"
            SELECT 
                status 
            FROM 
                friendships
            WHERE 
                (requester_id = @UserId AND accepter_id = @FriendId AND status = 1) -- ja sou amigo
            OR	(requester_id = @FriendId AND accepter_id = @UserId AND status = 1) -- ja sou amigo
            OR	(requester_id = @UserId AND accepter_id = @FriendId AND status = 3) -- ja esta pendendente
            OR 	(requester_id = @FriendId AND accepter_id = @UserId AND status = 3) -- ja esta pendente
            OR 	(requester_id = @UserId AND accepter_id = @FriendId AND status = 2) -- pessoa me bloqueou
            OR 	(requester_id = @UserId AND accepter_id = @FriendId); -- nao tem registro 
        ";

        var parameters = new
        {
            UserId = userId,
            FriendId = friendId
        };

        var result = await _connection.ExecuteScalarAsync<int?>(query, parameters);

        return result;
    }

    public async Task<int> CreateFriendshipInviteAsync(int userId, int friendId)
    {
        const string query = @"
            INSERT INTO friendships
                (status, requester_id, accepter_id)
            VALUES(3, @UserId, @FriendId);
        ";

        var parameters = new
        {
            UserId = userId,
            FriendId = friendId
        };

        var result = await _connection.ExecuteAsync(query, parameters);

        return result;
    }

    public async Task<Friendship?> GetFriendshipInviteById(int inviteId)
    {
        const string query = @"
            SELECT
                *
            FROM
                friendships
            WHERE
                friendship_id = @InviteId;
        ";

        var parameters = new
        {
            Inviteid = inviteId
        };

        var result = await _connection.QueryFirstOrDefaultAsync<Friendship>(query, parameters);

        return result;
    }

    public async Task<int> UpdateFriendshipInviteSituationAsync(int situationCode, int inviteId, int userId)
    {
        await using var transaction = await _connection.BeginTransactionAsync();

        try
        {
            const string query = @"
                UPDATE 
                    friendships
                SET 
                    status = @Situation
                WHERE 
                        friendship_id = @InviteId
                    AND accepter_id   = @UserId
                    AND status        = 3
        ";

            var parameters = new
            {
                Situation = situationCode,
                InviteId = inviteId,
                UserId = userId
            };

            var result = await _connection.ExecuteAsync(query, parameters);

            await transaction.CommitAsync();

            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<List<User?>> GetFriendsAsync(int userId)
    {
        const string query = @"
            SELECT 
                u.*
            FROM 
                users u	
            JOIN
                friendships f ON requester_id = @UserId OR accepter_id = @UserId
            WHERE
                    f.status = 1 
                AND u.id != @UserId;
        ";

        var parameters = new
        {
           UserId = userId 
        };

        var friends = await _connection.QueryAsync<User>(query, parameters);

        await _connection.CloseAsync();

        return friends.ToList()!;
    }
}