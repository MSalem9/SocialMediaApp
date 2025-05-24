using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Models;
using SocialMediaApp.Repository.Interfaces;

namespace SocialMediaApp.Repository.Repositores
{
    public class PostRepository : IPostRepository
    {
        SocialContext context;
        public PostRepository(SocialContext context) 
        {
            this.context = context;
        }

        public void Add(Post entity)
        {
            context.Posts.Add(entity);
        }

        public void Delete(long Id)
        {
            context.Posts.Remove(GetById(Id));
        }

        public List<Post> GetAll(string includes = null)
        {
            if (includes == null)
            {
                return context.Posts.ToList();
            }
            else
            {
                return context.Posts.Include(includes).ToList();
            }
        }

        public List<Post> GetPostsByOwnerId(long userId) 
        {
            return context.Posts.Where(p => p.UserId == userId).ToList();
        }

        public Post GetById(long id)
        {
            return context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Post entity)
        {
            Post post = GetById(entity.Id);

            post.Content = entity.Content;
            post.ImageId = entity.ImageId;
            post.PrivacyState = entity.PrivacyState; 

            context.Posts.Update(post);
        }

        public List<Post> GetPostsByPrivcayId(long privacyId)
        {
            return context.Posts.Where(p => p.PrivacyStateId == privacyId).ToList();
        }

        public List<Post> GetPostsByGroupId(long id)
        {
            return context.Posts.Where(p => p.GroupId == id).ToList();
        }

        //public Post GetFullPostsDetails(long userId) 
        //{
        //    var posts = context.Posts.Include()
        //}
    }
}
