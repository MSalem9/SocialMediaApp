using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Repository.Interfaces;

namespace SocialMediaApp.Controllers
{
    public class PostController : Controller
    {
        IPostRepository PostRepository;
        public IActionResult Index()
        {
            return View();
        }
    }
}
