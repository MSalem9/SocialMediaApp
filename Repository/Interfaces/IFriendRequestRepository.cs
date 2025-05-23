using SocialMediaApp.Models;

namespace SocialMediaApp.Repository.Interfaces
{
    public interface IFriendRequestRepository : IRepository<FriendRequest>
    {
        public string GetFriendRequestStatus(long reseiverId, long senderId);
        public void CancelingFriendRequest(long reseiverId, long senderId);
        public List<User> GetFriendRequestsUsers(long currentId);
        public void ChangeFriendRequestState(long reseiverId, long senderId, string state);
    }
}
