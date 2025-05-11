using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace SocialMediaApp.Models
{
    public class Post
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Group")]
        public long? GroupId { get; set; }
        public Group? Group { get; set; }

        [ForeignKey("PrivacyState")]
        public long PrivacyStateId { get; set; }
        public PrivacyState PrivacyState { get; set; }

        [ForeignKey("Image")]
        public long? ImageId { get; set; }
        public Image Image { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }

}
