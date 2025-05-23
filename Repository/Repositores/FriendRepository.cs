using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Models;
using SocialMediaApp.Repository.Interfaces;

namespace SocialMediaApp.Repository.Repositores
{
    public class FriendRepository : IFriendRepository
    {
        IUserRepository userRepository { get; set; }
        SocialContext context;

        public FriendRepository(SocialContext context, IUserRepository userRepository) 
        {
            this.userRepository = userRepository;
            this.context = context;
        }
        public void Add(Friend entity)
        {
            context.Friends.Add(entity);
        }

        public void Delete(long Id)
        {
            context.Friends.Remove(GetById(Id));
        }

        public List<Friend> GetAll(string includes = null)
        {
            if (includes == null)
            {
                return context.Friends.ToList();
            }
            else
            {
                return context.Friends.Include(includes).ToList();
            }
        }

        public Friend GetById(long id)
        {
            return context.Friends.FirstOrDefault(i => i.Id == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Friend entity)
        {
            Friend friend = GetById(entity.Id);

            friend.FriendId = entity.Id;
            friend.UserId = entity.UserId;  


            context.Friends.Update(friend);
        }
        public List<User> GetFriends(long currentId)
        {
            var friendsConnections = context.Friends.Where(i => i.UserId == currentId || i.FriendId == currentId).ToList();

            var friendsList = new List<User>();
            foreach (var item in friendsConnections) 
            {
                long friendID;
                if (item.UserId == currentId)
                {
                    friendID = item.FriendId;
                }
                else 
                {
                    friendID = item.UserId;
                }

                var user = userRepository.GetById(friendID);
                friendsList.Add(user);
            }

            return friendsList;
        }
    }
}
