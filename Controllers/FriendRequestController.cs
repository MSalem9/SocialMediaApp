using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SocialMediaApp.Models;
using SocialMediaApp.Repository.Interfaces;

namespace SocialMediaApp.Controllers
{
    public class FriendRequestController : ControllerBase
    {
        IFriendRequestRepository friendRequestRepository;
        IFriendRepository friendRepository;
        public FriendRequestController(IFriendRequestRepository friendRequestRepository, IFriendRepository friendRepository) 
        {
            this.friendRequestRepository = friendRequestRepository;
            this.friendRepository = friendRepository;
        }

        public IActionResult CreateFriendRequest(long receiverId) 
        {
            var friendRequest = new FriendRequest();

            friendRequest.ReceiverId = receiverId;
            friendRequest.SenderId = (long)GetCurrentUserId();
            friendRequest.Status = "Pending";

            friendRequestRepository.Add(friendRequest);
            friendRequestRepository.Save();

            string referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer)) 
            {
                return Redirect(referer);
            }

            return RedirectToAction("Index", "public");
        }

        [HttpPost]
        public IActionResult CancelFriendRequest(long receiverId)
        {
            friendRequestRepository.CancelingFriendRequest(receiverId, (long)GetCurrentUserId());

            string referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }

            return RedirectToAction("Index", "public");
        }

        [HttpPost]
        public IActionResult DeclineFriendRequest(long senderId)
        {
            friendRequestRepository.CancelingFriendRequest(senderId, (long)GetCurrentUserId());

            string referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }

            return RedirectToAction("Index", "public");
        }

        [HttpPost]
        public IActionResult AcceptFriendRequest(long senderId) 
        {
            var friendConnection = new Friend();
            var currentUserId = (long)GetCurrentUserId();

            friendConnection.UserId = currentUserId;
            friendConnection.FriendId = senderId;

            friendRepository.Add(friendConnection);
            friendRequestRepository.ChangeFriendRequestState(currentUserId, senderId, "Friends");
            friendRepository.Save();


            string referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }

            return RedirectToAction("Index", "public");
        }

    }

    
}
