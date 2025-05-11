using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Models;
using SocialMediaApp.Repository.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace SocialMediaApp.Repository.Repositores
{
    public class UserRepository : IUserRepository
    {
        SocialContext context;
        public UserRepository(SocialContext context) 
        {
            this.context = context;
        }

        public void Add(User entity)
        {
            context.Users.Add(entity);
        }

        public void Delete(long Id)
        {
            context.Users.Remove(GetById(Id));
        }

        public List<User> GetAll(string includes = null)
        {
            if (includes == null)
            {
                return context.Users.ToList();
            }
            else
            {
                return context.Users.Include(includes).ToList();
            }
        }

        public User GetById(long id)
        {
            return context.Users.FirstOrDefault(c => c.Id == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(User entity)
        {
            User user = GetById(entity.Id);

            user.Username = entity.Username;
            user.Password = entity.Password;
            user.Email = entity.Email;
            user.ProfilePicId = entity.ProfilePicId;
            user.CoverPicId = entity.CoverPicId;
            user.PrivacyState = entity.PrivacyState;

            context.Update(user);
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public User GetByEmail(string email)
        {
            return context.Users.FirstOrDefault(u => u.Email == email);
        }

        public class UserDetails
        {
            public string Name { get; set; }
            public string ImageUrl { get; set; }

        }
        //public UserDetails GetUserDetails(long UserId)
        //{
        //    var user = GetById(UserId);
        //    var userDetails = new UserDetails();


        //    userDetails.Name = GetById(UserId).Username;
        //    userDetails.ImageUrl = context.Images.FirstOrDefault(i => i.Id == user.CoverPicId).Url;

        //    return userDetails;
        //}

        IUserRepository.UserDetails IUserRepository.GetUserDetails(long UserId)
        {
            var user = GetById(UserId);
            var userDetails = new UserDetails();


            userDetails.Name = GetById(UserId).Username;
            userDetails.ImageUrl = context.Images.FirstOrDefault(i => i.Id == user.CoverPicId).Url;

            return null;
        }
    }
}
