using ITI_MVC.Models;

namespace ITI_MVC.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);

    }
}
