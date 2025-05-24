namespace SocialMediaApp.ViewModels
{
    public class GroupViewModel
    {
        public long? GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupCoverImageURL { get; set; }
        public List<PostCardViewModel> Posts { get; set; }

    }
}
