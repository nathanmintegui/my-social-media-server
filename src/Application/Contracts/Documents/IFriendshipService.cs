namespace Application.Contracts.Documents;

public interface IFriendshipService
{
   Task RequestFriendshipAsync(int userId, int friendId);
}