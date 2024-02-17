using Firesafe.Domain.Entities;
using Firesafe.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProvinceRepository(DbContext dbContext) : Repository<Province>(dbContext), IProvinceRepository
{
    public Province? GetById(int id)
    {
        return DbSet.FirstOrDefault(p => p.ProvinceId == id);
    }
}