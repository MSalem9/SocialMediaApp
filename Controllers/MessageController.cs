using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Models;
using SocialMediaApp.Repository.Interfaces;
using SocialMediaApp.ViewModels;



namespace SocialMediaApp.Controllers
{
    [Authorize]
    public class MessageController : ControllerBase
    {

        IMessageRepository messageRepository;
        IUserRepository userRepository;
        IImageRepository imageRepository;

        public MessageController(IMessageRepository messageRepository, IUserRepository userRepository, IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
            this.messageRepository = messageRepository;
            this.userRepository = userRepository;
        }

        public IActionResult Chat(long senderId, long receiverId)
        {
            var currentUserId = GetCurrentUserId(); // Implement this method

            if (senderId != currentUserId)
            {
                return Unauthorized(); // prevent access if senderId doesn't match logged-in user
            }

            var messages = messageRepository.GetMessagesBetweenUsers(senderId, receiverId);
            var users = userRepository.GetAll().ToDictionary(u => u.Id);

            var userImageId = userRepository.GetById(receiverId).ProfilePicId;
            var receiverImgURL = imageRepository.GetById((long)userImageId).Url;

            var viewModels = messages.Select(m => new MessageViewModel
            {
                Id = m.Id,
                SenderId = m.SenderId,
                ReceiverId = m.ReceiverId,
                Content = m.Content,
                SentAt = m.SentAt,
                SenderUsername = users.ContainsKey(m.SenderId) ? users[m.SenderId].Username : "Unknown",
                //SenderProfilePicUrl = users.ContainsKey(m.SenderId) && users[m.SenderId].ProfilePic != null
                //    ? users[m.SenderId].ProfilePic.Url
                //    : "BlankPic.jpg"
                SenderProfilePicUrl = receiverImgURL
            }).ToList();
            
            //var userImageId = userRepository.GetById(receiverId).ProfilePicId;

            //ViewBag.ReceiverImgURL = imageRepository.GetById(receiverId).Url;
            ViewBag.SenderId = senderId;
            ViewBag.ReceiverId = receiverId;

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult AllChats(long userId)
        {
            var currentUserId = GetCurrentUserId();
            if (userId != currentUserId)
            {
                return Unauthorized();
            }

            var messages = messageRepository.GetAllMessagesForUser(userId);

            // Get unique conversation partners (sender or receiver who is NOT the current user)
            var conversations = messages
                .Select(m => m.SenderId == userId ? m.Receiver : m.Sender)
                .DistinctBy(u => u.Id) // requires System.Linq.Dynamic.Core or implement manually
                .ToList();

            var viewModels = conversations.Select(user => new MessageViewModel
            {
                SenderId = userId,
                ReceiverId = user.Id,
                SenderUsername = user.Username,
                SenderProfilePicUrl = user.ProfilePic?.Url ?? "BlankPic.jpg",
                Content = messages
                    .Where(m => (m.SenderId == userId && m.ReceiverId == user.Id) || (m.SenderId == user.Id && m.ReceiverId == userId))
                    .OrderByDescending(m => m.SentAt)
                    .FirstOrDefault()?.Content,
                SentAt = messages
                    .Where(m => (m.SenderId == userId && m.ReceiverId == user.Id) || (m.SenderId == user.Id && m.ReceiverId == userId))
                    .OrderByDescending(m => m.SentAt)
                    .FirstOrDefault()?.SentAt ?? DateTime.UtcNow
            }).OrderByDescending(p => p.SentAt).ToList();

            ViewBag.SenderId = userId;
            return View("AllChats", viewModels);
        }

        [HttpPost]
        public IActionResult SendMessage(long senderId, long receiverId, string content)
        {
            var currentUserId = GetCurrentUserId();

            if (senderId != currentUserId)
            {
                return Unauthorized();
            }

            if (!string.IsNullOrWhiteSpace(content))
            {
                var message = new Message
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    Content = content,
                    SentAt = DateTime.UtcNow
                };

                messageRepository.Add(message);
                messageRepository.Save();
            }

            return RedirectToAction("Chat", new { senderId, receiverId });
        }
    }
}