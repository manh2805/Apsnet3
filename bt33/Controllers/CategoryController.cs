using bt33.Models;
using Microsoft.AspNetCore.Mvc;

namespace bt33.Controllers
{
    public class CategoryController : Controller
    {
        public static List<Category> categories = new List<Category>
        {
            new Category { Id = 1, Name = "Laptop" },
            new Category { Id = 2, Name = "Phone" },
            new Category { Id = 3, Name = "Tablet" }
        };

        // GET: Category
        [HttpGet]
        public ActionResult Index()
        {
            return View(categories);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (!ModelState.IsValid) return View(category);

            category.Id = categories.Count == 0 ? 1 : categories.Max(c => c.Id) + 1;
            categories.Add(category);
            return RedirectToAction(nameof(Index));
            //category.Id = categories.Count == 0 ? 1 : categories.Max(c => c.Id) + 1;
            //categories.Add(category);
            //return RedirectToAction(nameof(Index));
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {

            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
            if (id != category.Id) return BadRequest();
            if (!ModelState.IsValid) return View(category);

            var existing = categories.FirstOrDefault(c => c.Id == id);
            if (existing == null) return NotFound();

            existing.Name = category.Name;
            return RedirectToAction(nameof(Index));
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category != null) categories.Remove(category);
            return RedirectToAction(nameof(Index));
        }
    }
}
