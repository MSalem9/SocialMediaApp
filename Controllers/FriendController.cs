using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Models;
using SocialMediaApp.Repository.Interfaces;
using SocialMediaApp.Repository.Repositores;
using SocialMediaApp.Service;
using SocialMediaApp.ViewModels;

namespace SocialMediaApp.Controllers
{
    [Authorize]
    public class FriendController : ControllerBase
    {
        IPostService postService;
        IUserRepository userRepository;
        IImageRepository imageRepository;
        IFriendRequestRepository friendRequestRepository;
        IFriendRepository friendRepository;
        SocialContext context;
        public FriendController(IPostService postService, SocialContext context,
                                 IUserRepository userRepository, IImageRepository imageRepository,
                                 IFriendRequestRepository friendRequestRepository,
                                 IFriendRepository friendRepository)
        {
            this.friendRequestRepository = friendRequestRepository;
            this.friendRepository = friendRepository;
            this.imageRepository = imageRepository;
            this.userRepository = userRepository;
            this.postService = postService;
            this.context = context;
        }

        public ActionResult Index()
        {
            var currentUserId = (long)GetCurrentUserId();

            ViewBag.SenderId = currentUserId;//update added

            List<User> userList = friendRepository.GetFriends(currentUserId);
            List<SearchProfileCardViewModel> searchProfileCardViewModels = new List<SearchProfileCardViewModel>();

            foreach (User user in userList)
            {
                if (user.Id == currentUserId)
                {
                    continue;
                }

                searchProfileCardViewModels.Add(new SearchProfileCardViewModel
                {
                    UserId = user.Id,
                    UserName = user.Username,
                    UserImageUrl = user.ProfilePicId.HasValue
                                   ? imageRepository.GetById(user.ProfilePicId.Value)?.Url
                                   : "BlankPic.jpg",
                });
            }

            List<User> userRequestsList = friendRequestRepository.GetFriendRequestsUsers((long)GetCurrentUserId());
            List<SearchProfileCardViewModel> requestProfileCardViewModels = new List<SearchProfileCardViewModel>();

            foreach (User user in userRequestsList)
            {
                if (user.Id == GetCurrentUserId())
                {
                    continue;
                }

                string frindReqState;
                string getFrindReqState = friendRequestRepository.GetFriendRequestStatus(user.Id, (long)GetCurrentUserId());
                if (getFrindReqState == "Pending")
                {
                    frindReqState = getFrindReqState;
                }
                else
                {
                    continue;
                }

                requestProfileCardViewModels.Add(new SearchProfileCardViewModel
                {
                    UserId = user.Id,
                    UserName = user.Username,
                    UserImageUrl = user.ProfilePicId.HasValue
                                   ? imageRepository.GetById(user.ProfilePicId.Value)?.Url
                                   : "BlankPic.jpg",
                    FrindReqState = frindReqState,
                });
            }

            var friendsViewModel = new FriendsViewModel();

            friendsViewModel.searchProfileCardViewModelsList = searchProfileCardViewModels;
            friendsViewModel.friendRequestsList = requestProfileCardViewModels;

            return View("Index", friendsViewModel);
        }

    }
}
