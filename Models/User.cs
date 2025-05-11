using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace SocialMediaApp.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("ProfilePic")]
        public long? ProfilePicId { get; set; }
        public Image ProfilePic { get; set; }

        [ForeignKey("CoverPic")]
        public long? CoverPicId { get; set; }
        public Image CoverPic { get; set; }

        [ForeignKey("PrivacyState")]
        public long PrivacyStateId { get; set; }
        public PrivacyState PrivacyState { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Friend> Friends { get; set; }
        public ICollection<GroupMember> GroupMemberships { get; set; }
    }
}
