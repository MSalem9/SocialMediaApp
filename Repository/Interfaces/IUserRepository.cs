using SocialMediaApp.Models;
using static SocialMediaApp.Repository.Repositores.UserRepository;

namespace SocialMediaApp.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public User GetByEmail(string email);

        string HashPassword(string password);

        public List<User> SearchUsers(string term, int take = 10, int skip = 0);

    }
}
