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
    }

    public class ProfileController : ControllerBase
    {
        IPostService postService;
        IUserRepository userRepository;
        SocialContext context;
        public ProfileController(IPostService postService, SocialContext context, IUserRepository userRepository) 
        {
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

            var viewModels = new List<PostCardViewModel>();
            var posts = postService.GetProfileFeed(userId.Value);

            foreach (var post in posts)
            {
                //UserDetails userDetails = userRepository.GetUserDetails(post.UserId);

                viewModels.Add(new PostCardViewModel
                {
                    Content = post.Content,
                    TimePassed = GetTimePassedString(post.CreatedAt),
                    //OwnerName = userDetails.Name,
                    //OwnerImageURL = userDetails.ImageURL,
                    OwnerName = userRepository.GetById(post.UserId).Username,
                    //OwnerImageURL = context.Images.FirstOrDefault(i => i.Id == user.CoverPicId).Url,
                    comments = context.Comments.Where(c => c.PostId == post.Id).ToList(),
                    CommentsCount = context.Comments.Count(c => c.PostId == post.Id),
                });
            }

            return View("index", viewModels);
        }


        public string GetTimePassedString(DateTime createdAt)
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
}
