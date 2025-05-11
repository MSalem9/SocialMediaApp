using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaApp.Models
{
    public class FriendRequest
    {
        public long Id { get; set; }

        [ForeignKey("Sender")]
        public long SenderId { get; set; }
        public User Sender { get; set; }

        [ForeignKey("Receiver")]
        public long ReceiverId { get; set; }
        public User Receiver { get; set; }

        public string Status { get; set; } // "pending", "accepted", "rejected"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
