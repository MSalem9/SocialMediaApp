using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Models;
using SocialMediaApp.Repository.Interfaces;

namespace SocialMediaApp.Controllers
{
    [Authorize]
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
                
                //return RedirectToAction("Index", "public");


                string referer = Request.Headers["Referer"].ToString();
                if (!string.IsNullOrEmpty(referer))
                {
                    return Redirect(referer);
                }

                return RedirectToAction("Index", "public");
            }

            return RedirectToAction("Index", "public");
        }




    }
}
