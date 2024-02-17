using Firesafe.Domain.Core.Repository;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Repositories;

public interface IProvinceRepository : IRepository<Province>
{
    public Province? GetById(int id);
}