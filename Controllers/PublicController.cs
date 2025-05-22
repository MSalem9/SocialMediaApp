using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SocialMediaApp.Models;
using SocialMediaApp.Repository.Interfaces;
using SocialMediaApp.Service;
using SocialMediaApp.ViewModels;

namespace SocialMediaApp.Controllers
{
    [Authorize]
    public class PublicController : ControllerBase
    {
        IUserRepository userRepository;
        IPostService postService;
        IImageRepository imageRepository;
        SocialContext context;
        public PublicController(IUserRepository userRepository, IPostService postService, IImageRepository imageRepository, SocialContext context)
        {
            this.userRepository = userRepository;
            this.postService = postService;
            this.imageRepository = imageRepository;
            this.context = context;
        }
        public IActionResult Index()
        {
            try
            {

                var publicPosts = postService.GetPublicFeed();
                var viewModels = new List<PostCardViewModel>();
                string imageUrl;

                foreach (var publicPost in publicPosts)
                {
                    if(publicPost.ImageId != null) 
                    {
                        imageUrl = context.Images.FirstOrDefault(i => i.Id == publicPost.ImageId).Url;
                    }
                    else 
                    {
                        imageUrl = null;
                    }
                    var owner = userRepository.GetById(publicPost.UserId);
                    viewModels.Add(new PostCardViewModel
                    {
                        CurrentUserId = (long)GetCurrentUserId(),
                        PostOwnerId = publicPost.UserId,
                        PostId = (int?)publicPost.Id,
                        Content = publicPost.Content,
                        TimePassed = GetTimePassedString(publicPost.CreatedAt),
                        OwnerName = userRepository.GetById(publicPost.UserId).Username,
                        OwnerImageURL = imageRepository.GetProfileImage(owner),
                        OwnerCoverImageURL = context.Images.FirstOrDefault(i => i.Id == owner.CoverPicId).Url,
                        comments = context.Comments.Where(c => c.PostId == publicPost.Id).ToList(),
                        CommentsCount = context.Comments.Count(c => c.PostId == publicPost.Id),
                        PostImageUrl = imageUrl,
                        
                    });
                }


                return View("index", viewModels);
            }
            catch (Exception ex) 
            {
                return null;
            }
        }
    }
}
