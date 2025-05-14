using SocialMediaApp.Models;

namespace SocialMediaApp.ViewModels
{
    public class PostCardViewModel
    {
        public int? PostId { get; set; }
        public string? Content { get; set; }
        public string OwnerName { get; set; }
        public string? OwnerImageURL { get; set; }
        public string? OwnerCoverImageURL { get; set; }
        public string? PostImageUrl { get; set; }
        public int? CommentsCount { get; set; }
        public string TimePassed { get; set; }
        public List<Comment>? comments { get; set; }

        public string? CommentContent { get; set; }
        public string? CommentOwnerId { get; set; }


    }
}
