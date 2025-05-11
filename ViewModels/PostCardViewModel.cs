using SocialMediaApp.Models;

namespace SocialMediaApp.ViewModels
{
    public class PostCardViewModel
    {
        public string Content { get; set; }
        public string OwnerName { get; set; }
        public long? OwnerImageURL { get; set; }
        public int CommentsCount { get; set; }
        public string TimePassed { get; set; }
        public List<Comment>? comments { get; set; }

    }
}
