using Firesafe.Domain.Entities;
using Firesafe.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NewspaperCategoryRepository(DbContext dbContext) : Repository<NewspaperCategory>(dbContext), INewspaperCategoryRepository
{
    public bool IsExist(string category)
    {
        return DbSet.FirstOrDefault(nc => nc.NewspaperCategoryId == category) != null;
    }

    public NewspaperCategory? Get(string category)
    {
        return DbSet.FirstOrDefault(nc => nc.NewspaperCategoryId == category);
    }
}