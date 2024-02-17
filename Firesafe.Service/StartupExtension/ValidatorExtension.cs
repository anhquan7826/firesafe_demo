using Application.ViewModels.Requests.AddProduct;
using Application.ViewModels.Requests.DeleteNewspaper;
using Application.ViewModels.Requests.DeleteProduct;
using Application.ViewModels.Requests.EditProduct;
using Application.ViewModels.Requests.EditSupplierProfile;
using Application.ViewModels.Requests.EditUserProfile;
using Application.ViewModels.Requests.FcmToken;
using Application.ViewModels.Requests.GetNewspaper;
using Application.ViewModels.Requests.GetNewspaperList.GetAll;
using Application.ViewModels.Requests.GetNewspaperList.GetByCategory;
using Application.ViewModels.Requests.GetNewspaperList.GetByField;
using Application.ViewModels.Requests.GetNewspaperList.GetNew;
using Application.ViewModels.Requests.GetProductDetail;
using Application.ViewModels.Requests.GetProducts;
using Application.ViewModels.Requests.GetSupplierProducts;
using Application.ViewModels.Requests.GetSupplierProfile;
using Application.ViewModels.Requests.PutNewspaper;
using Application.ViewModels.Requests.SearchProducts;
using Application.ViewModels.Requests.SetRole;
using FluentValidation;

namespace Firesafe.Service.StartupExtension;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public static class ValidatorExtension
{
    internal static void AddValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<AddProductRequest>, AddProductValidator>();
        services.AddScoped<IValidator<EditProductRequest>, EditProductValidator>();
        services.AddScoped<IValidator<GetProductsRequest>, GetProductsValidator>();
        services.AddScoped<IValidator<SearchProductsRequest>, SearchProductValidator>();
        services.AddScoped<IValidator<SetRolesRequest>, SetRoleValidator>();
        services.AddScoped<IValidator<GetSupplierProductsRequest>, GetSupplierProductValidator>();
        services.AddScoped<IValidator<DeleteProductRequest>, DeleteProductValidator>();
        services.AddScoped<IValidator<EditProfileRequest>, EditProfileValidator>();
        services.AddScoped<IValidator<GetProductDetailRequest>, GetProductDetailValidator>();
        services.AddScoped<IValidator<GetSupplierProfileRequest>, GetSupplierProfileValidator>();
        services.AddScoped<IValidator<EditSupplierProfileRequest>, EditSupplierProfileValidator>();
        services.AddScoped<IValidator<GetNewspaperRequest>, GetNewspaperValidator>();
        services.AddScoped<IValidator<PutNewspaperRequest>, PutNewspaperValidator>();
        services.AddScoped<IValidator<DeleteNewspaperRequest>, DeleteNewspaperValidator>();
        services.AddScoped<IValidator<GetNewNewspapersRequest>, GetNewNewspapersValidator>();
        services.AddScoped<IValidator<GetAllNewspapersRequest>, GetAllNewspaperValidator>();
        services.AddScoped<IValidator<GetNewspapersByCategoryRequest>, GetNewspapersByCategoryValidator>();
        services.AddScoped<IValidator<GetNewspapersByFieldRequest>, GetNewspapersByFieldValidator>();
        services.AddScoped<IValidator<FcmTokenRequest>, FcmTokenValidator>();
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member