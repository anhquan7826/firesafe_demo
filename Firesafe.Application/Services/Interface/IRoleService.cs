using Firesafe.Domain.Entities;

namespace Application.Services.Interface;

public interface IRoleService
{
    public IEnumerable<string> GetAllRoles();

    public void SetRole(Guid userId, List<string> roles);
}