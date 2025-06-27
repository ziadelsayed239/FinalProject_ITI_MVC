using ITI_MVC.Data;
using ITI_MVC.Models;
using ITI_MVC.Repository.IRepository;
using ITI_MVC.Services.IServices;

namespace ITI_MVC.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> cartRepo;

        public ShoppingCartService(IRepository<ShoppingCart> cartRepo)
        {
            this.cartRepo = cartRepo;
        }

        public void AddToCart(string userId, int productId, int quantity)
        {
            var cartItem = cartRepo.Get(c => c.ApplicationUserId == userId && c.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Count += quantity;
            }
            else
            {
                cartRepo.Add(new ShoppingCart { ApplicationUserId = userId, ProductId = productId, Count = quantity });
            }
        }

        public void RemoveFromCart(string userId, int productId)
        {
            var cartItem = cartRepo.Get(c => c.ApplicationUserId == userId && c.ProductId == productId);
            if (cartItem != null)
                cartRepo.Remove(cartItem);
        }

        public IEnumerable<ShoppingCart> GetUserCart(string userId)
        {
            return cartRepo.GetAll(c => c.ApplicationUserId == userId, includeProperties: "Product");
        }

        public decimal GetCartTotal(string userId)
        {
            var cartItems = GetUserCart(userId);
            return cartItems.Sum(c => (decimal)c.Product.Price * c.Count);
        }

    }
}
