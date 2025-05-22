using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Models;
using SocialMediaApp.Repository.Interfaces;

namespace SocialMediaApp.Controllers
{
    public class CommentController : ControllerBase
    {
        ICommentRepository commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public IActionResult RemoveComment(long commentId) 
        {
            var comment = commentRepository.GetById(commentId);
            if (comment.UserId == GetCurrentUserId()) 
            {
                commentRepository.Delete(commentId);
                commentRepository.Save();
                
                return RedirectToAction("Index", "public");
            }

            return RedirectToAction("Index", "public");
        }




    }
}
