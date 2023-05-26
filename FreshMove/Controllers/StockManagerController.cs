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
            var categories = _context.Categories.OrderBy(c => c.Name).ToList();

            return View(categories);

        }

        [HttpGet]

        public IActionResult AddCategory()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCategory(Category model)
        {

            if(ModelState.IsValid)
            {

                Category category = new Category
                {

                    Name = model.Name,
                    Description = model.Description,
          

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
              
            };
            return View(newCategory);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCategory(EditCategoryVM model, string ID)
        {
            if(ModelState.IsValid)
            {
              
                var category = _context.Categories.Where(c => c.Id == ID).FirstOrDefault();
               
                 category.Name = model.Name;
                 category.Description = model.Description;

                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Categories");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory([FromRoute]string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var category = _context.Categories.Where(c => c.Id == Id && c.Archived == false).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


      
    }
}
