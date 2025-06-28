using ITI_MVC.Models;
using ITI_MVC.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITI_MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;

        public ProductController(IProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        public IActionResult Index()
        {
            var products = _productRepo.GetAll(includeProperties: "Category");
            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.CategoryList = _categoryRepo.GetAll()
                                     .Select(c => new SelectListItem
                                     {
                                         Text = c.Name,
                                         Value = c.Id.ToString()
                                     });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            var existingProduct = _productRepo.Get(p => p.Title == product.Title);

            if
                (existingProduct != null)
            {
                ModelState.AddModelError("Title", "Product with this title already exists.");
            }

            if (ModelState.IsValid)
            {
                _productRepo.Add(product);
                _productRepo.Save();
                TempData["success"] = "Product created successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryList = _categoryRepo.GetAll()
                                     .Select(c => new SelectListItem
                                     {
                                         Text = c.Name,
                                         Value = c.Id.ToString()
                                     });
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = _productRepo.Get(p => p.Id == id);
            if (product == null) return NotFound();

            ViewBag.CategoryList = _categoryRepo.GetAll()
                                     .Select(c => new SelectListItem
                                     {
                                         Text = c.Name,
                                         Value = c.Id.ToString()
                                     });

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {

            var existingProduct = _productRepo.Get(p => p.Title == product.Title && p.Id != product.Id);
            if (existingProduct != null)
            {
                ModelState.AddModelError("Title", "A product with this title already exists.");
            }

            if (ModelState.IsValid)
            {
                _productRepo.Update(product);
                _productRepo.Save();
                TempData["success"] = "Product updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoryList = _categoryRepo.GetAll()
                                     .Select(c => new SelectListItem
                                     {
                                         Text = c.Name,
                                         Value = c.Id.ToString()
                                     });

            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = _productRepo.Get(p => p.Id == id);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _productRepo.Get(p => p.Id == id);
            if (product == null) return NotFound();

            _productRepo.Remove(product);
            _productRepo.Save();
            TempData["success"] = "Product deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
