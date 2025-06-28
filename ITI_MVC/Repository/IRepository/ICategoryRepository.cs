using ITI_MVC.Models;

namespace ITI_MVC.Repository.IRepository
{
    public interface ICategoryRepository: IRepository<Category>
    {
            void Update(Category category);

    }
}
