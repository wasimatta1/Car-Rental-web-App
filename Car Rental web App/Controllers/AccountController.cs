using AutoMapper;
using Car_Rental_web_App.Models;
using Car_Rental_web_App.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_web_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper _mapper;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, IMapper mapper)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            _mapper = mapper;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await userManager.FindByEmailAsync(loginViewModel.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "User with the provided email does not exist.");
                return View(loginViewModel);
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(user, loginViewModel.Password);
            if (!isPasswordValid)
            {
                ModelState.AddModelError("", "Invalid password.");
                return View(loginViewModel);
            }

            var result = await signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(loginViewModel);
            }

            HttpContext.Session.SetString("UserId", user.Id);

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }
            var user = _mapper.Map<User>(registerViewModel);

            var result = await userManager.CreateAsync(user, registerViewModel.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerViewModel);
            }
            return RedirectToAction("Login", "Account");
        }

        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel verifyEmailViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(verifyEmailViewModel);
            }

            var user = await userManager.FindByEmailAsync(verifyEmailViewModel.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "User with the provided email does not exist.");
                return View(verifyEmailViewModel);
            }

            return RedirectToAction("ChangePassword", "Account", new { email = user.Email });
        }
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("VerifyEmail", "Account");
            }
            var model = new ChangePasswordViewModel { Email = email };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(changePasswordViewModel);
            }

            var user = await userManager.FindByEmailAsync(changePasswordViewModel.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "User with the provided Email does not exist.");
                return View(changePasswordViewModel);
            }

            var result = await userManager.RemovePasswordAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(changePasswordViewModel);
            }

            result = await userManager.AddPasswordAsync(user, changePasswordViewModel.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(changePasswordViewModel);
            }

            return RedirectToAction("Login", "Account");
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

    }
}
