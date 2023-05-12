using FreshMove.Constants;
using FreshMove.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using FreshMove.Models.categories;
using Microsoft.AspNetCore.Hosting;

namespace FreshMove.Controllers
{
    [Authorize(Roles = RoleConstants.StockManager)]
    public class StockManagerController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StockManagerController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Categories()
        {
            var categories = _context.Categories.OrderByDescending(c => c.Name).ToList();

            return View(categories);

        }

        [HttpGet]

        public IActionResult AddCategory()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCategory(CategoryViewModel model)
        {

            if(ModelState.IsValid)
            {

                string uniqueFileName = ProcessUploadedFile(model);

                Category category = new Category
                {

                    Name = model.Name,
                    Description = model.Description,
                    categoryImage = uniqueFileName,

                };

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Categories");
            }
         
            return View(model);
        }
        public IActionResult EditCategory([FromRoute] string ID)
        {
            var category= _context.Categories.Where(c=>c.Id==ID && c.Archived==false).FirstOrDefault();


            if (category == null)
            {
                return NotFound();
            }
            var newCategory = new EditCategoryVM
            {

                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ExistingImage = category.categoryImage
            };
            return View(newCategory);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCategory(EditCategoryVM model, string ID)
        {
            if(ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedEditFile(model);
                var category = _context.Categories.Where(c => c.Id == ID).FirstOrDefault();
               
                 category.Name = model.Name;
                 category.Description = model.Description;

                if (model.ExistingImage != null)
                {
                    if (category.categoryImage != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Category", model.ExistingImage);
                        System.IO.File.Delete(filePath);

                    }
                }
                category.categoryImage = uniqueFileName;

                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Categories");
            }

            return View(model);
        }


        private string ProcessUploadedFile(CategoryViewModel model)
        {
            string uniqueFileName = null;

            if (model.categoryImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Category");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.categoryImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.categoryImage.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        } 
        private string ProcessUploadedEditFile(EditCategoryVM model)
        {
            string uniqueFileName = null;

            if (model.categoryImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Category");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.categoryImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.categoryImage.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
