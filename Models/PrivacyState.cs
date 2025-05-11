namespace SocialMediaApp.Models
{
    public class PrivacyState
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<User> Users { get; set; }    
        public ICollection<Group> Groups { get; set; }

    }

}
