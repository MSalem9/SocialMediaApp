using SocialMediaApp.Models;
using static SocialMediaApp.Repository.Repositores.UserRepository;

namespace SocialMediaApp.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);
        public class UserDetails
        {
            public string Name { get; internal set; }
            public long? ImageURL { get; internal set; }
        }

        public UserDetails GetUserDetails(long UserId);
        string HashPassword(string password);
    }
}
