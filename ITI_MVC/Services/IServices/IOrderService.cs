using ITI_MVC.Models;

namespace ITI_MVC.Services.IServices
{
    public interface IOrderService
    {
        void CreateOrder(string userId);
        List<OrderHeader> GetOrdersByUser(string userId);

        OrderHeader GetOrderHeaderById(int id);
        List<OrderDetail> GetOrderDetailsByOrderId(int orderHeaderId);

    }

}
