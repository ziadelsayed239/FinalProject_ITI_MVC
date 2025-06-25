using ITI_MVC.Models;
using ITI_MVC.Repository.IRepository;
using ITI_MVC.Services.IServices;

namespace ITI_MVC.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<OrderHeader> _orderHeaderRepo;
        private readonly IRepository<OrderDetail> _orderDetailRepo;
        private readonly IShoppingCartService _cartService;

        public OrderService(IRepository<OrderHeader> orderHeaderRepo, IRepository<OrderDetail> orderDetailRepo, IShoppingCartService cartService)
        {
            _orderHeaderRepo = orderHeaderRepo;
            _orderDetailRepo = orderDetailRepo;
            _cartService = cartService;
        }

        public void CreateOrder(string userId)
        {
            var cartItems = _cartService.GetUserCart(userId);
            if (!cartItems.Any()) return;

            var total = cartItems.Sum(item => item.Product.Price * item.Count);

            var orderHeader = new OrderHeader
            {
                ApplicationUserId = userId,
                OrderDate = DateTime.Now,
                ShippingDate = DateTime.Now.AddDays(3),
                OrderTotal = total,
                Name = "Placeholder",
                PhoneNumber = "0000000000",
                StreetAddress = "Test Address",
                City = "Test City",
                State = "Test State",
                PostalCode = "00000"
            };

            _orderHeaderRepo.Add(orderHeader);

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    OrderHeader = orderHeader,
                    ProductId = item.ProductId,
                    Count = item.Count,
                    Price = item.Product.Price
                };

                _orderDetailRepo.Add(orderDetail);
            }

            _orderDetailRepo.Save();

            foreach (var item in cartItems)
            {
                _cartService.RemoveFromCart(userId, item.ProductId);
            }
        }

        public List<OrderHeader> GetOrdersByUser(string userId)
        {
            return _orderHeaderRepo.GetAll(o => o.ApplicationUserId == userId, includeProperties: "ApplicationUser").ToList();
        }

        public OrderHeader GetOrderHeaderById(int id)
        {
            return _orderHeaderRepo.Get(o => o.Id == id, includeProperties: "ApplicationUser");
        }

        public List<OrderDetail> GetOrderDetailsByOrderId(int orderHeaderId)
        {
            return _orderDetailRepo.GetAll(d => d.OrderHeaderId == orderHeaderId, includeProperties: "Product").ToList();
        }


    }
}

