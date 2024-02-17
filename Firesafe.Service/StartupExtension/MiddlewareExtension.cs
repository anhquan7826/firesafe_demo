using Application.Middlewares;
using Application.Middlewares.Impl;

namespace Firesafe.Service.StartupExtension;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public static class MiddlewareExtension
{
    internal static void AddMiddlewares(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            services.AddSingleton<IFirebaseAuth, FirebaseAuthImpl>();

        services.AddSingleton<ICloudStorage, GoogleCloudStorage>();
        services.AddSingleton<IGoogleSheets, GoogleSheets>();
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member