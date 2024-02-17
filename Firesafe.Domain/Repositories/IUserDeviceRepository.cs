using Firesafe.Domain.Core.Repository;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Repositories;

public interface IUserDeviceRepository : IRepository<UserDevice>
{
    public bool IsExist(Guid userId, string token);
    
    public void AddToken(Guid userId, string token);
    
    public void RemoveToken(Guid userId, string token);
}