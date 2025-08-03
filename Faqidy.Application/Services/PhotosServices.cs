using Faqidy.Application.Abstraction.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faqidy.Application.Services
{
    public class PhotosServices : IPhotosServices
    {
        private readonly string _uploadFolder;
        private readonly IConfiguration configuration;

        public PhotosServices(IConfiguration _configuration)
        {
            _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", _configuration["FileStorage:UploadedFolder"]);

            if(!Directory.Exists(_uploadFolder) )
            {
                Directory.CreateDirectory(_uploadFolder);
            }
            configuration = _configuration;
        }

        public async Task<List<string>> SavePhotosAsync(ICollection<IFormFile> photos)
        {
            var savePhotoPaths = new List<string>();
            if (photos == null || !photos.Any())
                return savePhotoPaths;

            foreach (var photo in photos)
            {
                if (photo == null)
                    continue;
                var allowExetentions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var photoExetention = Path.GetExtension(photo.FileName).ToLowerInvariant();
                if (!allowExetentions.Contains(photoExetention))
                {
                    throw new ArgumentException($"File type {photoExetention} is not allowd");
                }

                const long maxFileSize = 5 * 1024 * 1024;
                if (photo.Length > maxFileSize)
                    throw new ArgumentException("The file large than the maxmum lenght");

                var fileName = Guid.NewGuid().ToString() + photoExetention;
                var filePath = Path.Combine(_uploadFolder, fileName);

                using(var stream = new FileStream(filePath , FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }
                savePhotoPaths.Add(fileName);
            }

            return savePhotoPaths;
        }
        public string GetPhotoUrl(string photoPath)
            => $"{configuration["ApiUrl"]}/{configuration["FileStorage:UploadedFolder"]}/{photoPath}";

    
    }
}
