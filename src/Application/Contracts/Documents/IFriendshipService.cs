namespace Application.Contracts.Documents;

public interface IFriendshipService
{
   Task RequestFriendshipAsync(int userId, int friendId);
   Task AcceptFriendshipInviteAsync(int userId, int inviteId);
}