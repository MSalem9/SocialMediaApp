﻿using System.ComponentModel.DataAnnotations;

namespace SocialMediaApp.ViewModels
{
    public class UserSignInViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool? RememberMe { get; set; }
    }
}
