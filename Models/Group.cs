using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace SocialMediaApp.Models
{
    public class Group
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("GroupAdmin")]
        public long GroupAdminId { get; set; }
        public User GroupAdmin { get; set; }

        [ForeignKey("CoverPic")]
        public long? CoverPicId { get; set; }
        public Image CoverPic { get; set; }

        [ForeignKey("PrivacyState")]
        public long PrivacyStateId { get; set; }
        public PrivacyState PrivacyState { get; set; }

        public ICollection<GroupMember> Members { get; set; }
        public ICollection<Post> Posts { get; set; }
    }

}
