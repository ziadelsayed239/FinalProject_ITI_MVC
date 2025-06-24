using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITI_MVC.Controllers
{
    public class AccountController : Controller
    {
        UserManager user;
        public AccountController(UserManager user)
        {
            this.user = user;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
