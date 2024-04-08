namespace Domain.Contracts.Repositories;

public interface IFriendshipRepository
{
    Task<int?> GetFriendshipSituationAsync(int userId, int friendId);
    Task<int> CreateFriendshipInviteAsync(int userId, int friendId);
}