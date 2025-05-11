using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Models;
using SocialMediaApp.Repository.Interfaces;
using System.Security.Claims;
using SocialMediaApp.ViewModels;

namespace SocialMediaApp.Controllers
{
    public class UserController : Controller
    {
        IUserRepository userRepository;

        public UserController(IUserRepository userRepository) 
        {
            this.userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {

            //var privacyState = 
            //var userSignUp = new UserSignUpViewModel
            //{
            //    RolesList = roles
            //};
            return View("SignUp");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(UserSignUpViewModel userSignUp)
        {

            if (ModelState.IsValid)
            {
                // Hash the password before saving
                userSignUp.Password = userRepository.HashPassword(userSignUp.Password);

                // Save the user to the database
                User user = new User
                {
                    Username = $"{userSignUp.FirstName}.{userSignUp.LastName}".ToLower(),
                    Email = userSignUp.Email,
                    Password = userSignUp.Password,
                    PrivacyStateId = 1 //Public
                };

                userRepository.Add(user);
                userRepository.Save();

                return RedirectToAction("SignIn", "User");
            }
            return View("signUp", userSignUp);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View("signIn");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(UserSignInViewModel userSign)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = userRepository.HashPassword(userSign.Password);

                var userDB = userRepository.GetByEmail(userSign.Email);

                if (userDB != null)
                {
                    if (userDB.Password != hashedPassword)
                    {
                        ModelState.AddModelError("", "Invalid email or password.");
                        return View("signIn", userSign);
                    }
                    else
                    {
                        ClaimsIdentity claims = new(CookieAuthenticationDefaults.AuthenticationScheme);
                        claims.AddClaim(new Claim("Id", userDB.Id.ToString()));
                        claims.AddClaim(new Claim("UserName", userDB.Username.ToString()));
                        claims.AddClaim(new Claim("PrivacyState", userDB.PrivacyStateId.ToString()));

                        ClaimsPrincipal Principles = new ClaimsPrincipal(claims);

                        await HttpContext.SignInAsync(Principles);
                    }


                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }

            }

            return View("signIn", userSign);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("SignIn");
        }

        public IActionResult ResetPassword() 
        {
            return View("ResetPassword");
        }
    }
}
