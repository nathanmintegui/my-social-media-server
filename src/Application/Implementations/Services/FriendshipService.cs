using Application.Contracts.Documents;
using Domain.Contracts.Repositories;
using Domain.Models;
using static Domain.Models.Situation;

namespace Application.Implementations.Services;

public class FriendshipService : IFriendshipService
{
    private const int Success = 1;

    private readonly IFriendshipRepository _friendshipRepository;
    private readonly IUserRepository _userRepository;

    public FriendshipService(IFriendshipRepository friendshipRepository, IUserRepository userRepository)
    {
        _friendshipRepository = friendshipRepository;
        _userRepository = userRepository;
    }

    public async Task RequestFriendshipAsync(int userId, int friendId)
    {
        if (friendId == userId) throw new Exception("Id inválido");

        _ = await _userRepository.GetUserByIdAsync(friendId) ??
            throw new Exception($"Usuário com ID {userId} não encontrado.");

        int result;

        var friendship = await _friendshipRepository.GetFriendshipByUserIdAsync(userId, friendId);

        switch (friendship!.FriendshipId)
        {
            case (int)Accepted:
                throw new Exception("Você já é amigo deste usuário.");
            case (int)Blocked:
                throw new Exception("Convite inválido, você está bloqueado.");
            case (int)Pending:
                throw new Exception("Convite de amizade já solicitado.");
            case (int)Rejected:
                result = await _friendshipRepository.UpdateFriendshipSituationAsync(friendship.FriendshipId,
                    (int)Pending);
                if (result != Success)
                    throw new Exception("Ocorreu um erro ao realizar convite.");
                break;
            default:
                result = await _friendshipRepository.CreateFriendshipInviteAsync(userId, friendId);
                if (result != Success)
                    throw new Exception("Ocorreu um erro ao realizar convite.");
                break;
        }
    }

    public async Task AcceptFriendshipInviteAsync(int userId, int inviteId)
    {
        var friendshipInvite = await _friendshipRepository.GetFriendshipInviteById(inviteId) ??
                               throw new Exception("Esta solitação de amizade não existe para a sua conta.");

        if (friendshipInvite.Status != Pending)
            throw new Exception("Não é possível aceitar esta solicitação.");

        if (friendshipInvite.AccepterId != userId)
            throw new Exception("Solicitação de amizade inválida.");

        var result = await _friendshipRepository.UpdateFriendshipInviteSituationAsync((int)Accepted,
            inviteId, userId);

        if (result != Success) throw new Exception("Ocorreu um erro ao processar a requição;");
    }

    public async Task<List<User?>> ListFriendsAsync(int userId)
    {
        var friends = await _friendshipRepository.GetFriendsAsync(userId);

        return friends;
    }

    public async Task<List<User?>> ListInvitesAsync(int userId)
    {
        var invites = await _friendshipRepository.GetFriendshipInvitesAsync(userId);

        return invites;
    }

    public async Task BlockFriendAsync(int userId, int friendId)
    {
        if (friendId == userId) throw new Exception("Id inválido");

        _ = await _userRepository.GetUserByIdAsync(friendId) ??
            throw new Exception($"Usuário com ID {friendId} não encontrado.");

        var friendship = await _friendshipRepository.GetFriendshipByUserIdAsync(userId, friendId) ??
                         throw new Exception("Operação inválida, você não tem nenhum vínculo com este usuário.");

        if (friendship.Status != Accepted) throw new Exception("Operação inválida, você não é amigo deste usuário.");

        var result =
            await _friendshipRepository.UpdateFriendshipSituationAsync(friendship.FriendshipId, (int)Blocked);

        if (result != Success) throw new Exception("Ocorreu um erro ao bloquear o usuário.");
    }
}