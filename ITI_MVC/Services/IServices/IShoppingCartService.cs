using ITI_MVC.Models;

namespace ITI_MVC.Services.IServices
{
    public interface IShoppingCartService
    {
        void AddToCart(string userId, int productId, int quantity);
        void RemoveFromCart(string userId, int productId);
        IEnumerable<ShoppingCart> GetUserCart(string userId);
        decimal GetCartTotal(string userId);
    }
}
