using FirebaseAdmin;
using Firesafe.Domain.Entities;
using Google.Apis.Auth.OAuth2;
using Infrastructure.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using JsonReader = Application.Utils.JsonReader;

namespace Firesafe.Service.StartupExtension;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public static class AuthenticationExtension
{
    internal static void AddFirebaseAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var jsonReader = new JsonReader(File.ReadAllText(configuration["ServiceKeys"]!));
        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromJson(jsonReader.Value("GoogleCloud"))
        });
        services.AddTransient<IClaimsTransformation, IdClaimsTransformation>();
        services
            .AddAuthentication()
            .AddJwtBearer(
                JwtBearerDefaults.AuthenticationScheme,
                options =>
                {
                    options.Authority = configuration.GetSection("FirebaseJWT")["Authority"];
                    options.Audience = configuration.GetSection("FirebaseJWT")["Audience"];
                    options.TokenValidationParameters.ValidIssuer =
                        configuration.GetSection("FirebaseJWT")["Authority"];
                });
        services.AddScoped<IAuthorizationHandler, RequiredRolesHandler>();
        services.AddAuthorizationBuilder()
            .AddPolicy(
                FiresafePolicy.FirebaseAuthenticated,
                builder => builder.Requirements.Add(FiresafePolicy.Requirement(FiresafePolicy.FirebaseAuthenticated))
            )
            .AddPolicy(
                FiresafePolicy.MustBeRegistered,
                builder => builder.Requirements.Add(FiresafePolicy.Requirement(FiresafePolicy.MustBeRegistered))
            )
            .AddPolicy(
                FiresafePolicy.IsASupplier,
                builder => builder.Requirements.Add(FiresafePolicy.Requirement(FiresafePolicy.IsASupplier))
            );
    }

    internal static void UseFirebaseAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member