using ITI_MVC.Data;
using ITI_MVC.Models;
using ITI_MVC.Repository.IRepository;

namespace ITI_MVC.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

      
    }
}
