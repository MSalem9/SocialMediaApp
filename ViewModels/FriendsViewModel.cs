using SocialMediaApp.Models;

namespace SocialMediaApp.ViewModels
{
    public class FriendsViewModel
    {
        public List<SearchProfileCardViewModel> searchProfileCardViewModelsList { get; set; }
        public List<SearchProfileCardViewModel> friendRequestsList { get; set; }
    }
}
