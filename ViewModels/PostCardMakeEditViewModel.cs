using SocialMediaApp.Models;

namespace SocialMediaApp.ViewModels
{
    public class PostCardMakeEditViewModel
    {
        public string? Content { get; set; }
        public long OwnerId { get; set; }
        public long? GroupId { get; set; }
        public long privacyState { get; set; }
        public string? ImageURL { get; set; }
        public List <PrivacyState>? privacyStatesList { get; set; }

    }
}
