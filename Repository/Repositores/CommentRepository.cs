using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Models;
using SocialMediaApp.Repository.Interfaces;

namespace SocialMediaApp.Repository.Repositores
{
    public class CommentRepository : ICommentRepository
    {
        SocialContext context;
        public CommentRepository (SocialContext context)
        {
            this.context = context;
        }

        public void Add(Comment entity)
        {
            context.Comments.Add(entity);
        }

        public void Delete(long Id)
        {
            context.Comments.Remove(GetById(Id));
        }

        public List<Comment> GetAll(string includes = null)
        {
            if (includes == null)
            {
                return context.Comments.ToList();
            }
            else
            {
                return context.Comments.Include(includes).ToList();
            }
        }

        public Comment GetById(long id)
        {
            return context.Comments.FirstOrDefault(c => c.Id == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Comment entity)
        {
            Comment comment = GetById(entity.Id);

            comment.Content = entity.Content;

            context.Comments.Update(comment);
        }
    }
}
