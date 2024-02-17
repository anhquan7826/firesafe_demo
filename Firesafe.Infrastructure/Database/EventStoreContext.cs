using Firesafe.Domain.Entities.EventStore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class EventStoreContext(DbContextOptions<EventStoreContext> options) : DbContext(options)
{
    public required DbSet<ExceptionStore> ExceptionEvents { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ExceptionStore.ConfigureRelationship(modelBuilder.Entity<ExceptionStore>());
        base.OnModelCreating(modelBuilder);
    }
}