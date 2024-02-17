using Firesafe.Domain.Core.Command;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Commands;

public class AddNewUserCommand(string firebaseId) : Command<bool>
{
    public readonly string FirebaseId = firebaseId;
}