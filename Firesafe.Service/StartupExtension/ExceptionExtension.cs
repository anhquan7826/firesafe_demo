namespace Firesafe.Service.StartupExtension;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public static class ExceptionExtension
{
    internal static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errApp =>
        {
            
        });
        app.UseHsts();
    }    
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member