namespace SocialMediaApp.ViewModels
{
    public class MessageViewModel
    {

        public long Id { get; set; }
        public long SenderId { get; set; }
        public string SenderUsername { get; set; }
        public string SenderProfilePicUrl { get; set; }
        public long ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }

    }
}