namespace Domain.Contracts.Repositories;

public interface ILikeRepository
{
    Task CreateOrUpdateLikeByPostIdAsync(int postId, int userId);
}