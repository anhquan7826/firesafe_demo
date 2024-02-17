using System.Text.RegularExpressions;
using Application.Utils;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using JsonReader = Application.Utils.JsonReader;

namespace Application.Middlewares.Impl;

public partial class GoogleCloudStorage : ICloudStorage
{
    public string BucketName { get; init; }
    public ICloudStorage.IUserStorage UserStorage { get; init; }
    public ICloudStorage.ISupplierStorage SupplierStorage { get; init; }
    public ICloudStorage.IProductStorage ProductStorage { get; init; }
    public ICloudStorage.INewspaperStorage NewspaperStorage { get; init; }

    public GoogleCloudStorage(IConfiguration configuration)
    {
        var jsonReader = new JsonReader(File.ReadAllText(configuration["ServiceKeys"]!));
        var googleCredential = GoogleCredential.FromJson(jsonReader.Value("GoogleCloud"));
        BucketName = configuration["GoogleCloudStorageBucket"]!;
        var storageClient = StorageClient.Create(googleCredential);
        UserStorage = new UserStorageImpl(storageClient, BucketName);
        SupplierStorage = new SupplierStorageImpl(storageClient, BucketName);
        ProductStorage = new ProductStorageImpl(storageClient, BucketName);
        NewspaperStorage = new NewspaperStorageImpl(storageClient, BucketName);
    }

    private abstract partial class BaseStorage(StorageClient storageClient, string bucketName)
    {
        protected string UploadObject(Stream fileStream, string path, string contentType)
        {
            var dataObject = storageClient.UploadObject(
                bucketName,
                path,
                contentType,
                fileStream
            );
            return dataObject.MediaLink;
        }

        protected string? GetObjectUrl(string path)
        {
            try
            {
                var obj = storageClient.GetObject(bucketName, path);
                return obj?.MediaLink;
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected void DownloadObject(string path, Stream destination)
        {
            storageClient.DownloadObject(bucketName, path, destination);
        }

        protected void DeleteObject(string path, bool isFolder = false)
        {
            if (isFolder)
            {
                var objects = storageClient.ListObjects(bucketName, path);
        
                foreach (var storageObject in objects)
                {
                    storageClient.DeleteObject(bucketName, storageObject.Name);
                }
            } else
                storageClient.DeleteObject(bucketName, path);
        }

        protected MemoryStream ConvertToStream(IFormFile file)
        {
            var memStream = new MemoryStream();
            file.CopyTo(memStream);
            memStream.Position = 0;
            return memStream;
        }

        protected MemoryStream ConvertToJpgStream(IFormFile file)
        {
            var mStream = new MemoryStream();
            file.CopyTo(mStream);
            var extension = file.FileExtension();

            if (JpgRegex().IsMatch(extension)) return mStream;

            mStream.Position = 0;
            var image = Image.Load(mStream);
            image.Save(mStream, new JpegEncoder());
            return mStream;
        }

        [GeneratedRegex("jpe?g", RegexOptions.IgnoreCase, "en-US")]
        private static partial Regex JpgRegex();
    }

    private class UserStorageImpl(StorageClient storageClient, string bucketName)
        : BaseStorage(storageClient, bucketName), ICloudStorage.IUserStorage
    {
        private readonly string _contentType = "image/jpg";

        public void UploadUserImages(Guid id, IFormFile? avatar, IFormFile? banner)
        {
            if (avatar != null)
                UploadObject(ConvertToJpgStream(avatar), $"users/{id}/avatar.jpg", _contentType);
            if (banner != null)
                UploadObject(ConvertToJpgStream(banner), $"users/{id}/banner.jpg", _contentType);
        }

        public string? GetUserAvatarUrl(Guid id)
        {
            return GetObjectUrl($"users/{id}/avatar.jpg");
        }

        public string? GetUserBannerUrl(Guid id)
        {
            return GetObjectUrl($"users/{id}/banner.jpg");
        }

        public void DeleteUserFiles(Guid id)
        {
            DeleteObject($"users/{id}/", isFolder: true);
        }
    }

    private class SupplierStorageImpl(StorageClient storageClient, string bucketName)
        : BaseStorage(storageClient, bucketName), ICloudStorage.ISupplierStorage
    {
        private readonly string _contentType = "image/jpg";

        public void UploadSupplierImages(Guid id, IFormFile? avatar, IFormFile? banner)
        {
            if (avatar != null)
                UploadObject(ConvertToJpgStream(avatar), $"suppliers/{id}/avatar.jpg", _contentType);
            if (banner != null)
                UploadObject(ConvertToJpgStream(banner), $"suppliers/{id}/banner.jpg", _contentType);
        }

        public string? GetSupplierAvatarUrl(Guid id)
        {
            return GetObjectUrl($"suppliers/{id}/avatar.jpg");
        }

        public string? GetSupplierBannerUrl(Guid id)
        {
            return GetObjectUrl($"suppliers/{id}/banner.jpg");
        }

        public void DeleteSupplierFiles(Guid id)
        {
            DeleteObject($"suppliers/{id}/", isFolder: true);
        }
    }

    private class ProductStorageImpl(StorageClient storageClient, string bucketName)
        : BaseStorage(storageClient, bucketName), ICloudStorage.IProductStorage
    {
        private readonly string _contentType = "image/jpg";

        public void UploadProductImages(Guid id, IFormFile? thumbnail,
            Dictionary<Guid, IFormFile>? certificates, Dictionary<Guid, IFormFile>? images)
        {
            if (thumbnail != null)
                UploadObject(ConvertToJpgStream(thumbnail), $"products/{id}/thumbnail.jpg", _contentType);
            if (certificates != null)
                foreach (var certificate in certificates)
                    UploadObject(ConvertToJpgStream(certificate.Value),
                        $"products/{id}/certificates/{certificate.Key}.jpg", _contentType);
            if (images == null) return;
            foreach (var image in images)
                UploadObject(ConvertToJpgStream(image.Value), $"products/{id}/images/{image.Key}.jpg", _contentType);
        }

        public string? GetProductThumbnailUrl(Guid id)
        {
            return GetObjectUrl($"products/{id}/thumbnail.jpg");
        }

        public string? GetProductCertificateUrl(Guid productId, Guid certificateId)
        {
            return GetObjectUrl($"products/{productId}/certificates/{certificateId}.jpg");
        }

        public List<string?> GetProductCertificateUrls(Guid productId, List<Guid> certificateIds)
        {
            return certificateIds.Select(id => GetObjectUrl($"products/{productId}/certificates/{id}.jpg")).ToList();
        }

        public List<string?> GetProductImageUrls(Guid productId, List<Guid> imageIds)
        {
            return imageIds.Select(id => GetObjectUrl($"products/{productId}/images/{id}.jpg")).ToList();
        }

        public string? GetProductImageUrl(Guid productId, Guid imageId)
        {
            return GetObjectUrl($"products/{productId}/images/{imageId}.jpg");
        }

        public void DeleteProductImages(Guid productId, List<Guid> imageIds)
        {
            foreach (var id in imageIds) DeleteObject($"products/{productId}/images/{id}.jpg");
        }

        public void DeleteProductFiles(Guid id)
        {
            DeleteObject($"products/{id}/", isFolder: true);
        }
    }

    private class NewspaperStorageImpl(StorageClient storageClient, string bucketName)
        : BaseStorage(storageClient, bucketName), ICloudStorage.INewspaperStorage
    {
        public void UploadNewspaper(Guid id, HtmlDocument? content, IFormFile? thumbnail,
            Dictionary<Guid, IFormFile>? images)
        {
            if (content != null)
            {
                var memStream = new MemoryStream();
                content.Save(memStream);
                memStream.Position = 0;
                UploadObject(memStream, $"newspapers/{id}/content.html", "text/html");
            }

            if (thumbnail != null)
                UploadObject(ConvertToJpgStream(thumbnail), $"newspapers/{id}/thumbnail.jpg", "image/jpg");
            if (images != null)
                foreach (var image in images)
                    UploadObject(ConvertToJpgStream(image.Value), $"newspapers/{id}/images/{image.Key}.jpg",
                        "image/jpg");
        }

        public string? GetNewspaperContent(Guid id)
        {
            try
            {
                var memStream = new MemoryStream();
                DownloadObject($"newspapers/{id}/content.html", memStream);
                memStream.Position = 0;
                var reader = new StreamReader(memStream);
                return reader.ReadToEnd();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string? GetNewspaperThumbnailUrl(Guid id)
        {
            return GetObjectUrl($"newspapers/{id}/thumbnail.jpg");
        }

        public List<string?> GetNewspaperImageUrls(Guid newspaperId, List<Guid> imageIds)
        {
            return imageIds.Select(id => GetObjectUrl($"newspapers/{newspaperId}/images/{id}.jpg")).ToList();
        }

        public void DeleteNewspaperImages(Guid newspaperId, List<Guid> imageIds)
        {
            foreach (var id in imageIds) DeleteObject($"newspapers/{newspaperId}/images/{id}.jpg");
        }

        public void DeleteNewspaperFiles(Guid id)
        {
            DeleteObject($"newspapers/{id}/", isFolder: true);
        }
    }
}