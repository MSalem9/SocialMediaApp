using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Models;
using SocialMediaApp.Repository.Interfaces;

namespace SocialMediaApp.Service
{
    public interface IPostService
    {
        ICollection<Post> GetProfileFeed(long id);
    }

    internal class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly IUserRepository userRepository;

        public PostService(SocialContext context, IPostRepository postRepository, IUserRepository userRepository) 
        {
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

            return postRepository.GetPostsByOwnerId(user.Id);
        }

        public ICollection<Post> GetPublicFeed() 
        {
            return postRepository.GetPostsByPrivcayId(1);
        }

    }
}
