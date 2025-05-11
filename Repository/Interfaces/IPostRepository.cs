using SocialMediaApp.Models;
namespace SocialMediaApp.Repository.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        public List<Post> GetPostsByOwnerId(long userId);
        public List<Post> GetPostsByPrivcayId(long privacyId);
    }
}
