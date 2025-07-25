using Microsoft.AspNetCore.Http;

namespace Faqidy.Application.Abstraction.Services
{
    public interface IPhotosServices
    {
        Task<List<string>> SavePhotosAsync(ICollection<IFormFile> photos);
        string GetPhotoUrl(string photoPath);
    }
}
