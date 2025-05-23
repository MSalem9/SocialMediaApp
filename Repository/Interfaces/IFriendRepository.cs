using SocialMediaApp.Models;

namespace SocialMediaApp.Repository.Interfaces
{
    public interface IFriendRepository : IRepository<Friend>
    {
        public List<User> GetFriends(long curentId);
    }
}
