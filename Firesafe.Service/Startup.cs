using Firesafe.Service.StartupExtension;

namespace Firesafe.Service;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public class Startup(IConfiguration configuration, IWebHostEnvironment env)
{
    private IConfiguration Configuration { get; } = configuration;


    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddLogging(loggingBuilder => { loggingBuilder.AddConsole(); });
        services.AddUserSessions();
        services.AddDatabase(Configuration);
        services.AddMiddlewares(Configuration, env);
        services.AddApiVersion();
        services.AddCustomSwagger();
        services.AddMapper();
        services.AddMediator();
        services.AddInfrastructure();
        services.AddValidation();
        services.AddFirebaseAuth(Configuration);
    }

    public void Configure(IApplicationBuilder app)

    {
        app.UseRouting();
        app.UseFirebaseAuth();
        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
        app.UseUserSessions();
        app.UseEndpoints(endpoints =>
        {
            if (env.IsDevelopment())
                endpoints.MapControllerRoute(
                    "get-token",
                    "get-token"
                );
            endpoints.MapControllers();
        });
        app.UseHttpsRedirection();
        app.UseCustomSwagger();
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member