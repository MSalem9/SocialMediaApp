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
    public class GroupController : ControllerBase
    {
        IGroupRepository groupRepository;
        IImageRepository imageRepository;
        IPostService postService;
        IUserRepository userRepository;
        SocialContext context;

        public GroupController(IGroupRepository groupRepository, SocialContext socialContext,
                               IImageRepository imageRepository, IPostService postService, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.imageRepository = imageRepository;
            this.groupRepository = groupRepository;
            this.postService = postService;
            this.context = socialContext;
        }

        public ActionResult Index() 
        {
            var currentId = (long)GetCurrentUserId();
            var viewModels = new List<GroupInfo>();
            var meberOf = context.GroupMembers.Where(i => i.UserId == currentId).ToList();

            ViewBag.CurrentId = currentId;

            if (meberOf.Count > 0) 
            {
                foreach (var member in meberOf) 
                {
                    var group = groupRepository.GetById(member.GroupId);
                    var groupImageUrl = imageRepository.GetById((long)group.CoverPicId).Url;

                    var viewModel = new GroupInfo();
                    viewModel.Id = member.GroupId;
                    viewModel.Name = group.Name;
                    viewModel.Description = group.Description;
                    viewModel.ImageUrl = groupImageUrl;
                    viewModel.GroupAdminId = group.GroupAdminId;

                    viewModels.Add(viewModel);
                }
                return View("index", viewModels);
            }

            return View("index");
        }

        public IActionResult CreateGroup() 
        {
            var viewModel = new GroupCreateEditViewModel();
            viewModel.PrivacyStateList = context.PrivacyStates.ToList();

            return View("CreateGroup", viewModel);
        }
        [HttpPost]
        public IActionResult SaveNewGroup(GroupCreateEditViewModel viewModel, IFormFile ImageFile) 
        {
            var newGroup = new Group();

            if (viewModel.Name != null) 
            {
                newGroup.Name = viewModel.Name;
                newGroup.Description = viewModel.Description;
                newGroup.PrivacyStateId = viewModel.PrivacyStateId;
                newGroup.GroupAdminId = (long)GetCurrentUserId();

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
                    savedImage.PrivacyStateId = 1;
                    savedImage.UserId = (long)GetCurrentUserId();
                    imageRepository.Add(savedImage);
                    imageRepository.Save();

                    newGroup.CoverPicId = imageRepository.GetByUrl(savedImage.Url);
                }

                groupRepository.Add(newGroup);
                groupRepository.Save();

                var groupMember = new GroupMember();
                groupMember.UserId = (long)GetCurrentUserId();
                groupMember.GroupId = context.Groups.FirstOrDefault(i => i.GroupAdminId == groupMember.UserId 
                                                                      && i.Name == newGroup.Name).Id;
                context.GroupMembers.Add(groupMember);
                context.SaveChanges();

                return RedirectToAction("Index", "Public");

            }
            else
            {
                viewModel.PrivacyStateList = context.PrivacyStates.ToList();
                return View("CreateGroup", viewModel);
            }
        }

        public IActionResult GroupInfo(int groupId) 
        {

            try
            {
                var groupPosts = postService.GetgroupFeed(groupId);
                var posts = new List<PostCardViewModel>();
                var group = groupRepository.GetById(groupId);
                string imageUrl;

                foreach (var groupPost in groupPosts)
                {
                    if (groupPost.ImageId != null)
                    {
                        imageUrl = context.Images.FirstOrDefault(i => i.Id == groupPost.ImageId).Url;
                    }
                    else
                    {
                        imageUrl = null;
                    }
                    var owner = userRepository.GetById(groupPost.UserId);
                    posts.Add(new PostCardViewModel
                    {
                        CurrentUserId = (long)GetCurrentUserId(),
                        PostOwnerId = groupPost.UserId,
                        PostId = (int?)groupPost.Id,
                        Content = groupPost.Content,
                        TimePassed = GetTimePassedString(groupPost.CreatedAt),
                        OwnerName = userRepository.GetById(groupPost.UserId).Username,
                        OwnerImageURL = imageRepository.GetProfileImage(owner),
                        comments = context.Comments.Where(c => c.PostId == groupPost.Id).ToList(),
                        CommentsCount = context.Comments.Count(c => c.PostId == groupPost.Id),
                        PostImageUrl = imageUrl,

                    });

                }
                    var viewModel = new GroupViewModel();

                    viewModel.Posts = posts;
                    viewModel.GroupCoverImageURL = context.Images.FirstOrDefault(i => i.Id == group.CoverPicId).Url;
                    viewModel.GroupName = group.Name;
                    viewModel.GroupId = group.Id;


                return View("GroupInfo", viewModel);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
