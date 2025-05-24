using SocialMediaApp.Models;
namespace SocialMediaApp.Repository.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        List<Message> GetMessagesBetweenUsers(long senderId, long receiverId);
        List<Message> GetMessagesWithUserDetails(long senderId, long receiverId);
        List<Message> GetAllMessagesForUser(long userId);
    }
}