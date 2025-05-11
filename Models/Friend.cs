using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaApp.Models
{
    public class Friend
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public long FriendId { get; set; }
        public User FriendUser { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
