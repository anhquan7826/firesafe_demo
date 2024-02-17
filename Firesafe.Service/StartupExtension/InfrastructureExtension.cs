using Application.Services;
using Application.Services.Impl;
using Application.Services.Interface;
using Firesafe.Domain.UnitOfWork;
using Infrastructure.UnitOfWork;

namespace Firesafe.Service.StartupExtension;

internal static class InfrastructureExtension
{
    internal static void AddInfrastructure(this IServiceCollection services)
    {
        // Unit of work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<ISupplierService, SupplierService> ();
        services.AddScoped<IProvinceService, ProvinceService>();
        services.AddScoped<INewspaperService, NewspaperService>();
    }
}