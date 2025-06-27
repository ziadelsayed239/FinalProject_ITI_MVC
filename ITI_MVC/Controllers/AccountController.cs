using ITI_MVC.Models;
using ITI_MVC.Services.IServices;
using ITI_MVC.Utility;
using ITI_MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITI_MVC.Controllers
{
    public class AccountController : Controller
    {
        readonly UserManager<ApplicationUser> user;
        readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> user, SignInManager<ApplicationUser> signInManager)
        {
            this.user = user;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountViewModel accountuserVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appuser = new()
                {
                    UserName = accountuserVM.UserName,
                    Email = accountuserVM.Email,
                    PhoneNumber = accountuserVM.PhoneNumber,
                    Address = accountuserVM.Address,
                };

                IdentityResult result = await user.CreateAsync(appuser, accountuserVM.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(appuser, false);
                    await user.AddToRoleAsync(appuser, StaticDetails.Role_Customer);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                //  نجيب اليوزر كـ UserName أو Email
                ApplicationUser appuser = await user.FindByNameAsync(loginVM.UserName);
                if (appuser == null)
                {
                    appuser = await user.FindByEmailAsync(loginVM.UserName);
                }

                if (appuser != null)
                {
                    var result = await signInManager.PasswordSignInAsync(
                        appuser.UserName,
                        loginVM.Password,
                        loginVM.RememberMe,
                        lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                // لو مفيش يوزر أو كلمة السر غلط
                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(loginVM);
        }
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = user.GetUserId(User);
            var currentUser = await user.FindByIdAsync(userId);

            if (currentUser == null)
                return NotFound();

            var model = new AccountViewModel
            {
                UserName = currentUser.UserName,
                Email = currentUser.Email,
                PhoneNumber = currentUser.PhoneNumber,
                Address = currentUser.Address
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> IsUserExists(string username)
        {
            var existingUser = await user.FindByNameAsync(username);
            return Json(existingUser == null);
        }
    }
}
