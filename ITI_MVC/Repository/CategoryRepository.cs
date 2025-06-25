using ITI_MVC.Data;
using ITI_MVC.Models;
using ITI_MVC.Repository.IRepository;

namespace ITI_MVC.Repository
{
    public class CategoryRepository:Repository<Category>, ICategoryRepository   

    {

        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
