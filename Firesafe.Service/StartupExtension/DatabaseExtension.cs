using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Firesafe.Service.StartupExtension;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public static class DatabaseExtension
{
    internal static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Database"))
        );
        services.AddDbContext<EventStoreContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("EventStore"))
        );
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member