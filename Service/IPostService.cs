using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Models;
using SocialMediaApp.Repository.Interfaces;

namespace SocialMediaApp.Service
{
    public interface IPostService
    {
        ICollection<Post> GetProfileFeed(long id);
        ICollection<Post> GetPublicFeed();
        ICollection<Post> GetgroupFeed(long id);
    }

    internal class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly IUserRepository userRepository;
        private readonly IGroupRepository groupRepository;

        public PostService(SocialContext context, IPostRepository postRepository, IUserRepository userRepository, IGroupRepository groupRepository) 
        {
            this.groupRepository = groupRepository; 
            this.postRepository = postRepository;
            this.userRepository = userRepository;
        }

        public ICollection<Post> GetProfileFeed(long id)
        {
            User user = userRepository.GetById(id);
            if (user == null) 
            {
                return new List<Post>();
            }

            return postRepository.GetPostsByOwnerId(user.Id).OrderByDescending(p => p.CreatedAt).ToList();
        }

        public ICollection<Post> GetPublicFeed() 
        {
            return postRepository.GetPostsByPrivcayId(1).OrderByDescending(p => p.CreatedAt).ToList();
        }

        public ICollection<Post> GetgroupFeed(long id) 
        {
            Group group = groupRepository.GetById(id);
            if (group == null)
            {
                return new List<Post>();
            }
            
            return postRepository.GetPostsByGroupId(id).OrderByDescending(p => p.CreatedAt).ToList();
        }

    }
}
