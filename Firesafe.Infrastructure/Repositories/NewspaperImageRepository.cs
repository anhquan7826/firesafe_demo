using Firesafe.Domain.Entities;
using Firesafe.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NewspaperImageRepository(DbContext dbContext) : Repository<NewspaperImage>(dbContext), INewspaperImageRepository
{
    
}