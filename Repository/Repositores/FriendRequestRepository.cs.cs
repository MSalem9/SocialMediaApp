using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Models;
using SocialMediaApp.Repository.Interfaces;

namespace SocialMediaApp.Repository.Repositores
{
    public class FriendRequestRepository : IFriendRequestRepository
    {

        SocialContext context;
        IUserRepository userRepository;

        public FriendRequestRepository(SocialContext context, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.context = context;
        }
        public void Add(FriendRequest entity)
        {
            context.FriendRequests.Add(entity);
        }

        public void Delete(long Id)
        {
            context.FriendRequests.Remove(GetById(Id));
        }

        public List<FriendRequest> GetAll(string includes = null)
        {
            if (includes == null)
            {
                return context.FriendRequests.ToList();
            }
            else
            {
                return context.FriendRequests.Include(includes).ToList();
            }
        }

        public FriendRequest GetById(long id)
        {
            return context.FriendRequests.FirstOrDefault(i => i.Id == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(FriendRequest entity)
        {
            FriendRequest friendRequest = GetById(entity.Id);

            friendRequest.SenderId = entity.SenderId;
            friendRequest.ReceiverId = entity.ReceiverId;
            friendRequest.Status = entity.Status;




            context.FriendRequests.Update(friendRequest);
        }

        public string GetFriendRequestStatus(long reseiverId, long senderId)
        {
            FriendRequest friendRequest = context.FriendRequests
                                         .FirstOrDefault((i => (i.ReceiverId == reseiverId && i.SenderId == senderId) ||
                                                               (i.ReceiverId == senderId && i.SenderId == reseiverId)));

            if (friendRequest == null)
            {
                return "NotFriends";
            }

            return friendRequest.Status;
        }

        public void CancelingFriendRequest(long reseiverId, long senderId)
        {
            FriendRequest friendRequest = context.FriendRequests
                         .FirstOrDefault(i => i.ReceiverId == reseiverId && i.SenderId == senderId);

            Delete(friendRequest.Id);
            Save();
        }

        public List<User> GetFriendRequestsUsers(long currentId) 
        {
            var friendRequestList = context.FriendRequests
                                           .Where(i => i.ReceiverId == currentId && i.Status == "Pending")
                                           .ToList();

            var friendRequestsUsersList = new List<User>();
            foreach (var friendRequest in friendRequestList) 
            {
                User user = userRepository.GetById(friendRequest.SenderId);
                friendRequestsUsersList.Add(user);
            }

            return friendRequestsUsersList;
        }

        public void ChangeFriendRequestState(long reseiverId, long senderId, string state) 
        {
            var friendRequest = context.FriendRequests.FirstOrDefault(i => i.ReceiverId == reseiverId && senderId == i.SenderId);

            friendRequest.Status = state;

            Update(friendRequest);
            Save();
        }

    }


}
