using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Logging;

namespace Freelancing.Services
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        private readonly ILogger<CloudinaryService> _logger;

        public CloudinaryService(IConfiguration configuration, ILogger<CloudinaryService> logger)
        {
            _logger = logger;
            var account = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]
            );
            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }

        public async Task<string> UploadImageAsync(IFormFile file, string folder = "chat_images")
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    _logger.LogWarning("No file provided for upload");
                    return null;
                }

                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation()
                        .Width(800)
                        .Height(800)
                        .Crop("limit")
                        .Quality("auto")
                        .FetchFormat("auto"),
                    Folder = folder,
                    PublicId = Guid.NewGuid().ToString()
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.LogError("Cloudinary upload failed: {Error}", uploadResult.Error?.Message);
                    throw new Exception($"Failed to upload image: {uploadResult.Error?.Message}");
                }

                _logger.LogInformation("Image uploaded successfully to {Url}", uploadResult.SecureUrl);
                return uploadResult.SecureUrl.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading image to Cloudinary");
                throw new Exception("Failed to upload image", ex);
            }
        }

        public async Task DeleteImageAsync(string publicId)
        {
            try
            {
                if (string.IsNullOrEmpty(publicId))
                {
                    _logger.LogWarning("No public ID provided for deletion");
                    return;
                }

                var deletionParams = new DeletionParams(publicId);
                var result = await _cloudinary.DestroyAsync(deletionParams);

                if (result.Result != "ok")
                {
                    _logger.LogWarning("Failed to delete image {PublicId} from Cloudinary: {Result}", publicId, result.Result);
                }
                else
                {
                    _logger.LogInformation("Image {PublicId} deleted successfully", publicId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting image {PublicId} from Cloudinary", publicId);
            }
        }

        public string GetPublicIdFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            try
            {
                var uri = new Uri(url);
                var segments = uri.AbsolutePath.Split('/');
                var fileName = segments.Last();
                return $"{segments[segments.Length - 2]}/{Path.GetFileNameWithoutExtension(fileName)}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error extracting public ID from URL {Url}", url);
                return null;
            }
        }
    }
}