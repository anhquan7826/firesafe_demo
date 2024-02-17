using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;

namespace Application.Middlewares;

public interface ICloudStorage
{
    public string BucketName { get; init; }

    public IUserStorage UserStorage { get; init; }
    public ISupplierStorage SupplierStorage { get; init; }
    public IProductStorage ProductStorage { get; init; }
    public INewspaperStorage NewspaperStorage { get; init; }


    interface IUserStorage
    {
        public void UploadUserImages(Guid id, IFormFile? avatar, IFormFile? banner);

        public string? GetUserAvatarUrl(Guid id);

        public string? GetUserBannerUrl(Guid id);

        public void DeleteUserFiles(Guid id);
    }

    interface ISupplierStorage
    {
        public void UploadSupplierImages(Guid id, IFormFile? avatar, IFormFile? banner);

        public string? GetSupplierAvatarUrl(Guid id);

        public string? GetSupplierBannerUrl(Guid id);

        public void DeleteSupplierFiles(Guid id);
    }

    interface IProductStorage
    {
        public void UploadProductImages(Guid id, IFormFile? thumbnail = null,
            Dictionary<Guid, IFormFile>? certificates = null,
            Dictionary<Guid, IFormFile>? images = null);

        public string? GetProductThumbnailUrl(Guid id);

        public string? GetProductCertificateUrl(Guid productId, Guid certificateId);
        public List<string?> GetProductCertificateUrls(Guid productId, List<Guid> certificateIds);

        public List<string?> GetProductImageUrls(Guid productId, List<Guid> imageIds);
        public string? GetProductImageUrl(Guid productId, Guid imageId);

        public void DeleteProductImages(Guid productId, List<Guid> imageIds);

        public void DeleteProductFiles(Guid id);
    }

    interface INewspaperStorage
    {
        public void UploadNewspaper(Guid id, HtmlDocument? content, IFormFile? thumbnail,
            Dictionary<Guid, IFormFile>? images);

        public string? GetNewspaperContent(Guid id);

        public string? GetNewspaperThumbnailUrl(Guid id);

        public List<string?> GetNewspaperImageUrls(Guid newspaperId, List<Guid> imageIds);

        public void DeleteNewspaperImages(Guid newspaperId, List<Guid> imageIds);

        public void DeleteNewspaperFiles(Guid id);
    }
}