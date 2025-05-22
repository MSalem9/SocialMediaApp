using NuGet.Protocol.Core.Types;
using SocialMediaApp.Migrations;
using SocialMediaApp.Models;
using SocialMediaApp.Repository.Interfaces;
using System.Drawing;
using System.Security.Policy;

namespace SocialMediaApp.Repository.Repositores
{
    public class ImageRepository : IImageRepository
    {
        SocialContext context;
        public ImageRepository(SocialContext context) 
        {
            this.context = context;
        }
        public void Add(Models.Image entity)
        {
            context.Images.Add(entity);
        }

        public void Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public List<Models.Image> GetAll(string includes = null)
        {
            throw new NotImplementedException();
        }

        public Models.Image GetById(long id)
        {
            return context.Images.FirstOrDefault(i => i.Id == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Models.Image entity)
        {
            throw new NotImplementedException();
        }

        public string GetProfileImage(User owner) 
        {
            if (owner.ProfilePicId == null) 
            {
                return null;
            }
            return context.Images.FirstOrDefault(i => i.Id == owner.ProfilePicId).Url;
        }

        public long GetByUrl(string url) 
        {
            if (url == null) 
            {
                return 0;
            }
            return context.Images.FirstOrDefault(i => i.Url == url).Id;
        }

        public List<Models.Image> GetImagesByOwnerId(long ownerId) 
        {
            return context.Images.Where(i => i.UserId == ownerId)
                .OrderByDescending(p => p.CreatedAt)
                .Take(5)
                .ToList();
        }

        public long SaveExternalImage(IFormFile ImageFile, long ownerId)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }
                Models.Image savedImage = new Models.Image();
                savedImage.Url = fileName;
                savedImage.PrivacyStateId = 1;
                savedImage.UserId = ownerId;
                Add(savedImage);
                Save();

                return GetByUrl(savedImage.Url);
            }
            else 
            {
                return 6;
            }
        }
    }
}
