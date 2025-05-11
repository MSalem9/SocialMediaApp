using SocialMediaApp.Models;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaApp.ViewModels
{
    public class UserSignUpViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Url(ErrorMessage = "Enter a valid image URL")]
        public string? PersonalImage { get; set; }

        // Foreign Key
        [Required(ErrorMessage = "Role is required")]
        public long PrivacyState { get; set; }

        public ICollection<PrivacyState>? PrivacyStateList { get; set; }
    }
}
