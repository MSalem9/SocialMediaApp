using SocialMediaApp.Models;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaApp.ViewModels
{
    public class ProfileEditViewModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? PasswordConfirm { get; set; }
        public string? OwnerImageURL { get; set; }
        public string? OwnerCoverImageURL { get; set; }
        public long? PrivacyStateId { get; set; }

        public List<PrivacyState>? privacyStatesList { get; set; }
    }
}
