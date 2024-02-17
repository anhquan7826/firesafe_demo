namespace Firesafe.Service.StartupExtension;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public static class UserSessionExtension
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
{
    internal static void AddUserSessions(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        services.AddSession(cfg =>
        {
            cfg.Cookie.Name = "Firesafe.Cookie";
            cfg.IdleTimeout = new TimeSpan(0, 10, 0);
        });
    }

    internal static void UseUserSessions(this IApplicationBuilder app)
    {
        app.UseSession();
    }
}