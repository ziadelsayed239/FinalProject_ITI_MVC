using ITI_MVC.Data;
using ITI_MVC.Models;
using ITI_MVC.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITI_MVC.Controllers
{
    [Authorize]
    // عشان لازم يكون logged in
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;
        

        public ShoppingCartController(IShoppingCartService cartService, UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }

        // GET: /ShoppingCart/
        public async Task<IActionResult> Index()
        {
            // Get logged-in user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            var cartItems = _cartService.GetUserCart(user.Id);
            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            _cartService.AddToCart(user.Id, productId, quantity);
            return RedirectToAction(nameof(Index));
        }

        // POST: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            _cartService.RemoveFromCart(user.Id, productId);
            return RedirectToAction(nameof(Index));
        }

        

    }
}
