using bt33.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace bt33.Controllers
{
    public class ProductController : Controller
    {
        public static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Dell XPS 13", Price = 30000000, Description = "Laptop mỏng nhẹ", CategoryId = 1 },
            new Product { Id = 2, Name = "iPhone 15", Price = 20000000, Description = "Điện thoại Apple", CategoryId = 2 },
            new Product { Id = 3, Name = "iPad Air", Price = 15000000, Description = "Máy tính bảng Apple", CategoryId = 3 }
        };

        private void LoadCategories(int? selectedId = null)
        {
            ViewBag.Categories = new SelectList(CategoryController.categories, "Id", "Name", selectedId);
        }

        // Gán navigation property Category theo CategoryId (dữ liệu đang lưu trong List nên phải tự gán)
        private static void LoadCategory(Product product)
        {
            product.Category = CategoryController.categories.FirstOrDefault(c => c.Id == product.CategoryId);
        }

        // GET: Product
        [HttpGet]
        public ActionResult Index()
        {
            products.ForEach(LoadCategory);
            return View(products);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            LoadCategory(product);
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            LoadCategories();
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (!CategoryController.categories.Any(c => c.Id == product.CategoryId))
                ModelState.AddModelError(nameof(Product.CategoryId), "Danh mục không tồn tại");

            if (!ModelState.IsValid)
            {
                LoadCategories(product.CategoryId);
                return View(product);
            }

            product.Id = products.Count == 0 ? 1 : products.Max(p => p.Id) + 1;
            products.Add(product);
            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            LoadCategories(product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            if (id != product.Id) return BadRequest();

            if (!CategoryController.categories.Any(c => c.Id == product.CategoryId))
                ModelState.AddModelError(nameof(Product.CategoryId), "Danh mục không tồn tại");

            if (!ModelState.IsValid)
            {
                LoadCategories(product.CategoryId);
                return View(product);
            }

            var existing = products.FirstOrDefault(p => p.Id == id);
            if (existing == null) return NotFound();

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Description = product.Description;
            existing.CategoryId = product.CategoryId;
            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            LoadCategory(product);
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null) products.Remove(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
