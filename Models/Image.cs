using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaApp.Models
{
    public class Image
    {
        public long Id { get; set; }
        public string Url { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }
        public User User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("PrivacyState")]
        public long PrivacyStateId { get; set; }
        public PrivacyState PrivacyState { get; set; }
    }

}
