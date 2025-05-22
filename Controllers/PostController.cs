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

        public IActionResult DeletePost(long postId) 
        {
            var post = postRepository.GetById(postId);
            post.Comments = context.Comments.Where(c => c.PostId == post.Id).ToList();

            if (post != null)
            {
                if (post.UserId == GetCurrentUserId())
                {
                    foreach (var comment in post.Comments) 
                    {
                        commentRepository.Delete(comment.Id);
                    }
                    postRepository.Delete(postId);

                    commentRepository.Save();
                    postRepository.Save();

                    return RedirectToAction("Index", "public");
                }
            }

            return RedirectToAction("index", "public");
        }

        public IActionResult EditPost(long postId) 
        {
            var postDB = postRepository.GetById(postId);
            if (postDB != null && postDB.UserId == GetCurrentUserId()) 
            {
                var postVM = new PostCardMakeEditViewModel();

                postVM.Content = postDB.Content;
                postVM.Id = postId;
                if (postDB.ImageId != null) 
                {
                    postVM.ImageURL = imageRepository.GetById((long)postDB.ImageId).Url;
                }
                postVM.privacyState = postDB.PrivacyStateId;
                postVM.privacyStatesList = context.PrivacyStates.ToList();

                return View("EditPost", postVM);
            }
            return RedirectToAction("index", "public");
        }
        [HttpPost]
        public IActionResult SaveEditPost(PostCardMakeEditViewModel postVM, IFormFile postImageFile)
        {
            var postDB = postRepository.GetById(postVM.Id);
            if (postDB == null || postDB.UserId != GetCurrentUserId())
            {
                return RedirectToAction("Index", "Public");
            }

            try
            {
                if (string.IsNullOrWhiteSpace(postVM.Content))
                {
                    postVM.privacyStatesList = context.PrivacyStates.ToList();
                    if (postDB.ImageId != null)
                    {
                        postVM.ImageURL = imageRepository.GetById((long)postDB.ImageId).Url;
                    }
                    return View("EditPost", postVM);
                }

                postDB.Content = postVM.Content;
                postDB.PrivacyStateId = postVM.privacyState;

                if (postImageFile != null)
                {
                    postDB.ImageId = imageRepository.SaveExternalImage(postImageFile, postDB.UserId);
                }

                postRepository.Update(postDB);
                postRepository.Save();

                return RedirectToAction("Index", "Public");
            }
            catch (Exception ex)
            {
                postVM.privacyStatesList = context.PrivacyStates.ToList();
                if (postDB.ImageId != null)
                {
                    postVM.ImageURL = imageRepository.GetById((long)postDB.ImageId).Url;
                }

                return View("EditPost", postVM);
            }
        }



    }
}
