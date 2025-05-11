using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaApp.Models
{
    public class Comment
    {
        public long Id { get; set; }
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        //ForeignKeys

        [ForeignKey("Post")]
        public long PostId { get; set; }
        public Post Post { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }

    }

}
