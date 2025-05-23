namespace SocialMediaApp.ViewModels
{
    public class ProfileViewModel
    {
        public long? UserId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerImageURL { get; set; }
        public string OwnerCoverImageURL { get; set; }
        public List<Models.Image>? OwnerImagesList { get; set; }
        public List<PostCardViewModel> Posts { get; set; }

        public string? FrindReqState { get; set; }
    }
}
