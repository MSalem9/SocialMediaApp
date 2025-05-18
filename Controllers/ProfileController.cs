using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Models;
using SocialMediaApp.Repository.Interfaces;
using SocialMediaApp.Repository.Repositores;
using SocialMediaApp.Service;
using SocialMediaApp.ViewModels;
using static SocialMediaApp.Repository.Interfaces.IUserRepository;
using static SocialMediaApp.Repository.Repositores.UserRepository;

namespace SocialMediaApp.Controllers
{
    public class ControllerBase : Controller
    {
        protected long? GetCurrentUserId()
        {
            var userIdClaim = this.User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null)
            {
                return null;
            }

            if(long.TryParse(userIdClaim.Value, out var Id))
            {
                return Id;
            }

            return null;
        }
        protected string GetTimePassedString(DateTime createdAt)
        {
            TimeSpan timePassed = DateTime.Now - createdAt;

            if (timePassed.TotalMinutes < 1)
            {
                return "just now";
            }
            else if (timePassed.TotalHours < 1)
            {
                int minutes = (int)timePassed.TotalMinutes;
                return $"{minutes} {(minutes == 1 ? "minute" : "minutes")} ago";
            }
            else if (timePassed.TotalDays < 1)
            {
                int hours = (int)timePassed.TotalHours;
                return $"{hours} {(hours == 1 ? "hour" : "hours")} ago";
            }
            else if (timePassed.TotalDays < 30)
            {
                int days = (int)timePassed.TotalDays;
                return $"{days} {(days == 1 ? "day" : "days")} ago";
            }
            else if (timePassed.TotalDays < 365)
            {
                int months = (int)(timePassed.TotalDays / 30);
                return $"{months} {(months == 1 ? "month" : "months")} ago";
            }
            else
            {
                int years = (int)(timePassed.TotalDays / 365);
                return $"{years} {(years == 1 ? "year" : "years")} ago";
            }
        }

    }

    public class ProfileController : ControllerBase
    {
        IPostService postService;
        IUserRepository userRepository;
        IImageRepository imageRepository;
        SocialContext context;
        public ProfileController(IPostService postService, SocialContext context, IUserRepository userRepository, IImageRepository imageRepository) 
        {
            this.imageRepository = imageRepository; 
            this.userRepository = userRepository;
            this.postService = postService;
            this.context = context;
        }

        public IActionResult Index()
        {

            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var viewModel = new ProfileViewModel();
            var postsList = new List<PostCardViewModel>();
            var posts = postService.GetProfileFeed(userId.Value);
            var owner = userRepository.GetById(userId.Value);
            string postImageUrl;

            foreach (var post in posts)
            {
                if (post.ImageId != null)
                {
                    postImageUrl = context.Images.FirstOrDefault(i => i.Id == post.ImageId).Url;
                }
                else
                {
                    postImageUrl = null;
                }

                postsList.Add(new PostCardViewModel
                {
                    Content = post.Content,
                    TimePassed = GetTimePassedString(post.CreatedAt),
                    OwnerName = userRepository.GetById(post.UserId).Username,
                    OwnerImageURL = context.Images.FirstOrDefault(i => i.Id == owner.ProfilePicId).Url,
                    OwnerCoverImageURL = context.Images.FirstOrDefault(i => i.Id == owner.CoverPicId).Url,
                    comments = context.Comments.Where(c => c.PostId == post.Id).ToList(),
                    CommentsCount = context.Comments.Count(c => c.PostId == post.Id),
                    PostImageUrl = postImageUrl,
                    PostId = post.Id,
                });
            }

            viewModel.Posts = postsList;
            viewModel.OwnerName = owner.Username;
            viewModel.OwnerImageURL = context.Images.FirstOrDefault(i => i.Id == owner.ProfilePicId).Url;
            viewModel.OwnerCoverImageURL = context.Images.FirstOrDefault(i => i.Id == owner.CoverPicId).Url;

            return View("index", viewModel);
        }

        public IActionResult UserSearch(string name) 
        {
            List<User> userList = userRepository.SearchUsers(name);
            List<SearchProfileCardViewModel> searchProfileCardViewModels = new List<SearchProfileCardViewModel>();


            if (userList.Count() == 0)
            {
                return RedirectToAction("Index", "public");
            }
            foreach (User user in userList) 
            {

                searchProfileCardViewModels.Add(new SearchProfileCardViewModel
                {
                    UserId = user.Id,
                    UserName = user.Username,
                    UserImageUrl = user.ProfilePicId.HasValue
                                   ? imageRepository.GetById(user.ProfilePicId.Value)?.Url
                                   : "BlankPic.jpg",

                });
                    
                
            }

            return View("UserSearch", searchProfileCardViewModels);
        }

        public IActionResult OtherProfile(long userId) 
        {
            
            if (userId == null)
            {
                return RedirectToAction("index", "public");
            }

            var owner = userRepository.GetById(userId);
            string friendReqState = string.Empty;
            var posts = new List<Post>();

            if (owner.PrivacyStateId == 1) 
            {
                posts = (List<Post>)postService.GetProfileFeed(userId);
            }
            else if (owner.PrivacyStateId == 3 && friendReqState == "Friends")
            {
                posts = (List<Post>)postService.GetProfileFeed(userId);
            }

            var viewModel = new ProfileViewModel();
            var postsList = new List<PostCardViewModel>();
            string postImageUrl;

            foreach (var post in posts)
            {
                if (post.PrivacyStateId != 1) 
                {
                    continue;
                }
                if (post.ImageId != null)
                {
                    postImageUrl = context.Images.FirstOrDefault(i => i.Id == post.ImageId).Url;
                }
                else
                {
                    postImageUrl = null;
                }

                postsList.Add(new PostCardViewModel
                {
                    Content = post.Content,
                    TimePassed = GetTimePassedString(post.CreatedAt),
                    OwnerName = userRepository.GetById(post.UserId).Username,
                    OwnerImageURL = context.Images.FirstOrDefault(i => i.Id == owner.ProfilePicId).Url,
                    OwnerCoverImageURL = context.Images.FirstOrDefault(i => i.Id == owner.CoverPicId).Url,
                    comments = context.Comments.Where(c => c.PostId == post.Id).ToList(),
                    CommentsCount = context.Comments.Count(c => c.PostId == post.Id),
                    PostImageUrl = postImageUrl,
                    PostId = post.Id,
                });  
            }
                viewModel.Posts = postsList;
                viewModel.OwnerName = owner.Username;
                viewModel.OwnerImageURL = context.Images.FirstOrDefault(i => i.Id == owner.ProfilePicId).Url;
                viewModel.OwnerCoverImageURL = context.Images.FirstOrDefault(i => i.Id == owner.CoverPicId).Url;

            return View("OtherProfile", viewModel);  
        }




       
    }
}
