using Application.Contracts.Documents;
using Domain.Contracts.Repositories;
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

        var friendshipSituationCode = await _friendshipRepository.GetFriendshipSituationAsync(userId, friendId);
        switch (friendshipSituationCode)
        {
            case (int)Accepted:
                throw new Exception("Você já é amigo deste usuário.");
            case (int)Blocked:
                throw new Exception("Convite inválido, você está bloqueado.");
            case (int)Pending:
                throw new Exception("Convite de amizade já solicitado.");
            case (int)Rejected:
                result = await _friendshipRepository.CreateFriendshipInviteAsync(userId, friendId);
                if (result != Success)
                    throw new Exception("Ocorreu um erro ao realizar convite.");
                break;
            case null:
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
}