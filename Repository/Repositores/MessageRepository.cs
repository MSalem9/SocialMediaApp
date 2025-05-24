using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Models;
using SocialMediaApp.Repository.Interfaces;

namespace SocialMediaApp.Repository.Repositores
{
    public class MessageRepository : IMessageRepository
    {

        SocialContext context;

        public MessageRepository(SocialContext context)
        {
            this.context = context;
        }

        public void Add(Message entity)
        {
            context.Messages.Add(entity);
        }

        public void Delete(long id)
        {
            context.Messages.Remove(GetById(id));
        }

        public List<Message> GetAll(string includes = null)
        {
            if (includes == null)
            {
                return context.Messages.ToList();
            }
            else
            {
                return context.Messages.Include(includes).ToList();
            }
        }

        public Message GetById(long id)
        {
            return context.Messages.FirstOrDefault(m => m.Id == id);
        }

        public List<Message> GetMessagesBetweenUsers(long senderId, long receiverId)
        {
            return context.Messages
                .Where(m =>
                    (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                    (m.SenderId == receiverId && m.ReceiverId == senderId))
                .OrderBy(m => m.SentAt)
                .ToList();
        }

        public List<Message> GetMessagesWithUserDetails(long senderId, long receiverId)
        {
            return context.Messages
                .Where(m =>
                    (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                    (m.SenderId == receiverId && m.ReceiverId == senderId))
                .Include(m => m.Sender)
                    .ThenInclude(u => u.ProfilePic)
                .Include(m => m.Receiver)
                    .ThenInclude(u => u.ProfilePic)
                .OrderBy(m => m.SentAt)
                .ToList();
        }
        public List<Message> GetAllMessagesForUser(long userId)
        {
            return context.Messages
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .Include(m => m.Sender).ThenInclude(u => u.ProfilePic)
                .Include(m => m.Receiver).ThenInclude(u => u.ProfilePic)
                .OrderBy(m => m.SentAt)
                .ToList();
        }
        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Message entity)
        {
            Message message = GetById(entity.Id);

            message.Content = entity.Content;

            context.Messages.Update(message);
        }
    }
}