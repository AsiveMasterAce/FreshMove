using FreshMove.Constants;
using FreshMove.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using FreshMove.Models.categories;
using Microsoft.AspNetCore.Hosting;
using FreshMove.Helpers;
using FreshMove.Models.users;
using Microsoft.EntityFrameworkCore;

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
            var categories = _context.Categories.Where(c=>c.Archived==false).OrderBy(c => c.Name).ToList();

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
            var category = _context.Categories.Where(c => c.Id == Id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> confirmDelete(string Id)
        {
            var category = _context.Categories.Where(c => c.Id == Id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();

            }
            else
            {
                category.Archived = true;

                _context.Categories.Update(category);

               await _context.SaveChangesAsync();
                return RedirectToAction("Categories");
            }
          
        }
        public async Task<IActionResult> StockEmployees(string sortOrder, string currentFilter, string searchString, int? pageNumber, Gender gender, UserRole userRole)
        {
            ViewData["CurrentSort"] = sortOrder;

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "Name";
            ViewData["LNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_descLast" : "LastName";
            ViewData["LNameDescParm"] = String.IsNullOrEmpty(sortOrder) ? "LastName" : "LastName";
            ViewData["RoleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "userRole" : "Role";
            ViewData["GenderSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Gender" : "Gender_desc";
            ViewData["GenderDescSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Gender" : "Gender_desc";
            ViewData["EmailSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Email" : "";

            var stockTaker= userRole.CompareTo(UserRole.StockTaker);


            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var users = from u in _context.Users
                        where u.Archived == false && u.UserRole== UserRole.StockTaker
                        select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.LastName.Contains(searchString) || u.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(_u => _u.FirstName);
                    break;
                case "name_descLast":
                    users = users.OrderByDescending(_u => _u.LastName);
                    break;
                case "LastName":
                    users = users.OrderBy(_u => _u.LastName);
                    break;
                case "userRole":
                    users = users.OrderByDescending(_u => ((int)_u.UserRole));
                    break;
                case "Role":
                    users = users.OrderBy(_u => ((int)_u.UserRole));
                    break;
                case "Gender":
                    users = users.OrderBy(_u => ((int)_u.Gender));
                    break;
                case "Gender_desc":
                    users = users.OrderByDescending(_u => ((int)_u.Gender));
                    break;
                case "Email":
                    users = users.OrderBy(_u => _u.Email);
                    break;
                default:
                    users = users.OrderBy(_u => _u.FirstName);
                    break;

            }

            int pageSize = 8;
            return View(await PaginatedList<ApplicationUser>.CreateAsync(users.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

    }
}
