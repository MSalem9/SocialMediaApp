using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Models;
using SocialMediaApp.Repository.Interfaces;
using SocialMediaApp.ViewModels;

namespace SocialMediaApp.Controllers
{
    public class PostController : ControllerBase
    {
        IPostRepository postRepository;
        ICommentRepository commentRepository;
        IImageRepository imageRepository;
        SocialContext context;

        public PostController(IPostRepository postRepository, IImageRepository imageRepository, ICommentRepository commentRepository, SocialContext context) 
        {
            this.context = context;
            this.postRepository=postRepository;
            this.commentRepository=commentRepository;
            this.imageRepository = imageRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewPost()
        {
            var viewModel = new PostCardMakeEditViewModel();
            
            viewModel.privacyStatesList = context.PrivacyStates.ToList();

            return View("NewPost", viewModel);
        }

        [HttpPost]
        public IActionResult SaveNewPost(PostCardMakeEditViewModel viewModel, IFormFile ImageFile) 
        {
            var newPost = new Post();
            if (viewModel.Content != null || viewModel.ImageURL != null) 
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ImageFile.CopyTo(stream);
                    }
                    Image savedImage = new Image();
                    savedImage.Url = fileName;
                    savedImage.PrivacyStateId = viewModel.privacyState;
                    savedImage.UserId = (long)GetCurrentUserId();
                    imageRepository.Add(savedImage);
                    imageRepository.Save();

                    newPost.ImageId = imageRepository.GetByUrl(savedImage.Url);
                }
               
                newPost.Content = viewModel.Content;
                newPost.PrivacyStateId = viewModel.privacyState;
                newPost.GroupId = null;
                newPost.UserId = (long)GetCurrentUserId();


                postRepository.Add(newPost);
                postRepository.Save();
                return RedirectToAction("Index", "Public");
            }
            else 
            {
                viewModel.privacyStatesList = context.PrivacyStates.ToList();
                return View("NewPost", viewModel);
            }
        }

        [HttpPost]
        public IActionResult SaveNewComment(long postId, string CommentContent)

        {
            var newComment = new Comment();

            if (CommentContent != null)
            {
                newComment.Content = CommentContent;
                newComment.PostId = postId;
                newComment.UserId = (long)GetCurrentUserId();

                commentRepository.Add(newComment);
                commentRepository.Save();

                return RedirectToAction("index", "public");

            }

            return RedirectToAction("index", "public");

        }

        public IActionResult GetOtherProfile(string name) 
        {

            return null;
        }
    }
}
