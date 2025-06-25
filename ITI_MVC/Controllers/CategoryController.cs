using ITI_MVC.Data;
using ITI_MVC.Models;
using ITI_MVC.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ITI_MVC.Controllers
{
    public class CategoryController : Controller
    {


        private readonly ICategoryRepository CategoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            CategoryRepo = categoryRepo;
        }


        public IActionResult index()
        {

            var categories = CategoryRepo.GetAll();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Category category)


        {

            var existingCategory = CategoryRepo.Get(c => c.Name.ToLower() == category.Name.ToLower());
            if (existingCategory != null)
            {
                ModelState.AddModelError("Name", "Category with this name already exists.");
            }

            if (ModelState.IsValid)
            {
                CategoryRepo.Add(category);
                CategoryRepo.Save();
                return RedirectToAction(nameof(index));
            }
            return View(category);
        }


        public IActionResult Edit(int id)
        {

            var category = CategoryRepo.Get(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }




        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Category category)
        {

            var existingCategory = CategoryRepo.Get(c => c.Name.ToLower() == category.Name.ToLower() && c.Id != category.Id);
            if (existingCategory != null)
            {
                ModelState.AddModelError("Name", "Category with this name already exists.");
            }

            if (ModelState.IsValid)
            {
                CategoryRepo.Update(category);
                CategoryRepo.Save();
                return RedirectToAction(nameof(index));
            }
            return View(category);


        }

        public IActionResult Delete(int id)
        {
            var category = CategoryRepo.Get(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        [HttpDelete, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteConfrim(int id)
        {
            var catefory = CategoryRepo.Get(c => c.Id == id);
            if (catefory == null)
            {
                return NotFound();
            }
            CategoryRepo.Remove(catefory);
            CategoryRepo.Save();
            return RedirectToAction(nameof(index));


        }
    }

}

