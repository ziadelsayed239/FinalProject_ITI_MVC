using ITI_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITI_MVC.Controllers
{
    public class AccountController : Controller
    {
        readonly UserManager<ApplicationUser> user;
        public AccountController(UserManager<ApplicationUser> user)
        {
            this.user = user;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

    }
}
