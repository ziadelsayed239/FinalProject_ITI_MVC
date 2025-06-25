using ITI_MVC.Models;
using ITI_MVC.Services.IServices;
using ITI_MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITI_MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        public IActionResult PlaceOrder()
        {
            var userId = _userManager.GetUserId(User);
            _orderService.CreateOrder(userId);
            return RedirectToAction("MyOrders");
        }

        public IActionResult MyOrders()
        {
            var userId = _userManager.GetUserId(User);
            var orders = _orderService.GetOrdersByUser(userId);
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var orderHeader = _orderService.GetOrderHeaderById(id);
            var orderDetails = _orderService.GetOrderDetailsByOrderId(id);

            if (orderHeader == null || orderDetails == null)
            {
                return NotFound(); 
            }

            var viewModel = new OrderDetailsViewModel
            {
                OrderHeader = orderHeader,
                OrderDetails = orderDetails
            };

            return View(viewModel);
        }


    }
}
