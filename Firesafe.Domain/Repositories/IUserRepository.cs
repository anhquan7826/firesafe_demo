using Firesafe.Domain.Core.Repository;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    public User? GetByFirebaseId(string id);

    public User AddNewUser(string firebaseId);

    public void SetRoles(Guid userId, List<string> roles);

    public User? SetInfo(Guid userId, string? name);
}