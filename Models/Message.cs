using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaApp.Models
{
    public class Message
    {
        public long Id { get; set; }
        public string Content { get; set; }

        [ForeignKey("Sender")]
        public long SenderId { get; set; }
        public User Sender { get; set; }

        [ForeignKey("Receiver")]
        public long ReceiverId { get; set; }
        public User Receiver { get; set; }

        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }

}
