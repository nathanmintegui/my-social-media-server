using Domain.Models;

namespace Domain.Contracts.Repositories;

public interface IFriendshipRepository
{
    Task<int?> GetFriendshipSituationAsync(int userId, int friendId);
    Task<int> CreateFriendshipInviteAsync(int userId, int friendId);
    Task<Friendship?> GetFriendshipInviteById(int inviteId);
    Task<int> UpdateFriendshipInviteSituationAsync(int situationCode, int inviteId, int userId);
    Task<List<User?>> GetFriendsAsync(int userId);
}