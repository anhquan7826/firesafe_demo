using Firesafe.Domain.Entities;
using Firesafe.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NewspaperRepository(DbContext dbContext) : Repository<Newspaper>(dbContext), INewspaperRepository
{
    
}