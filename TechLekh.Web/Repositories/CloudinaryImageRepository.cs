using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using TechLekh.Web.Models.Config;

namespace TechLekh.Web.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly Account _account;
        private readonly CloudinarySettings _cloudinarySettings;

        public CloudinaryImageRepository(IOptions<CloudinarySettings> options)
        {
            this._cloudinarySettings = options.Value;
            this._account = new Account(_cloudinarySettings.CloudName, 
                _cloudinarySettings.ApiKey, _cloudinarySettings.ApiSecret);
        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(_account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };

            var uploadResult = await client.UploadAsync(uploadParams);

            if (uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }
            return null;
        }
    }
}
