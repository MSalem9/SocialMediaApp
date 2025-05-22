using SocialMediaApp.Models;
using System.Drawing;

namespace SocialMediaApp.Repository.Interfaces
{
    public interface IImageRepository : IRepository<Models.Image>
    {
        public string GetProfileImage(User owner);
        public long GetByUrl(string url);
        public List<Models.Image> GetImagesByOwnerId(long ownerId);
        public long SaveExternalImage(IFormFile ImageFile, long ownerId);
    }
}
