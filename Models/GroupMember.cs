using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaApp.Models
{
    public class GroupMember
    {
        public long Id { get; set; }

        [ForeignKey("Group")]
        public long GroupId { get; set; }
        public Group Group { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}
