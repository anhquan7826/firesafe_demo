using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;

namespace Application.Middlewares;

public interface ICloudStorage
{
    public string BucketName { get; init; }

    // public string UploadObject(Stream stream, string path, string contentType);
    // public Task<string> UploadObjectAsync(Stream stream, string path, string contentType);
    //
    // public string UploadImage(IFormFile file, string path);
    // public Task<string> UploadImageAsync(IFormFile file, string path);
    //
    // public string UploadHtml(IFormFile file, string path);
    // public Task<string> UploadHtmlAsync(IFormFile file, string path);
    //
    // public string? GetHtml(string path);
    // public Task<string?> GetHtmlAsync(string path);
    //
    // public string? GetObjectUrl(string path);
    // public Task<string?> GetObjectUrlAsync(string path);
    //
    // public void DeleteObject(string path);
    // public Task DeleteObjectAsync(string path);

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
        public void UploadProductImages(Guid id, IFormFile? thumbnail, Dictionary<Guid, IFormFile>? images);

        public string? GetProductThumbnailUrl(Guid id);

        public List<string?> GetProductImageUrls(Guid productId, List<Guid> imageIds);

        public void DeleteProductImages(Guid productId, List<Guid> imageIds);

        public void DeleteProductFiles(Guid id);
    }

    interface INewspaperStorage
    {
        public void UploadNewspaper(Guid id, HtmlDocument? content, IFormFile? thumbnail, Dictionary<Guid, IFormFile>? images);

        public string? GetNewspaperContent(Guid id);
        
        public string? GetNewspaperThumbnailUrl(Guid id);

        public List<string?> GetNewspaperImageUrls(Guid newspaperId, List<Guid> imageIds);

        public void DeleteNewspaperImages(Guid newspaperId, List<Guid> imageIds);

        public void DeleteNewspaperFiles(Guid id);
    }
}