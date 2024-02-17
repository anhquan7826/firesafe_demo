using Firesafe.Domain.Entities;
using Firesafe.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductCategoryRepository(DbContext dbContext) : Repository<Category>(dbContext), IProductCategoryRepository
{
    public bool IsExist(string category)
    {
        return DbSet.FirstOrDefault(c => c.CategoryId == category) != null;
    }

    public Category? Get(string category)
    {
        return DbSet.FirstOrDefault(c => c.CategoryId == category);
    }
}