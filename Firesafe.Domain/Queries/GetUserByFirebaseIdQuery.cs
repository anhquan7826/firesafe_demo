using Firesafe.Domain.Core.Query;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Queries;

public class GetUserByFirebaseIdQuery(string firebaseId) : Query<User?>
{
    public readonly string FirebaseId = firebaseId;
}