using FreshMove.Constants;
using FreshMove.Data;
using FreshMove.Helpers;
using FreshMove.Models.users;
using FreshMove.Models.ViewModels;
using FreshMove.Models.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Cryptography;

namespace FreshMove.Controllers
{
    [Authorize(Roles = RoleConstants.Admin)]
   
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RegisterController> _logger;

     
        public AdminController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context, ILogger<RegisterController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
        }



        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult>Users(string sortOrder,string currentFilter,string searchString,int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "Name";

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
                        where u.Archived==false
                        select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.LastName.Contains(searchString)||u.FirstName.Contains(searchString));
            }
            
            int pageSize = 8;
            return View( await PaginatedList<ApplicationUser>.CreateAsync(users.AsNoTracking(), pageNumber ?? 1, pageSize));
        }   

        public IActionResult NewUser()
        {
            ViewBag.Users = _context.Users.OrderBy(u=>u.FirstName).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateNewUser(ApplicationUser model)
        {
            model.UserName = model.Email;
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(model, model.Password);

                if (result.Succeeded)
                {
                    if (model.UserRole == UserRole.StockManager)
                    {
                      await _userManager.AddToRoleAsync(model, RoleConstants.StockManager);
                    } 
                    else if (model.UserRole == UserRole.SalesManager) 
                    {
                        await _userManager.AddToRoleAsync(model, RoleConstants.SalesManager);
                    } 
                    else if (model.UserRole == UserRole.Admin)
                    {
                        await _userManager.AddToRoleAsync(model, RoleConstants.Admin);
                    }
                    else if(model.UserRole == UserRole.StockTaker)
                    {
                        await _userManager.AddToRoleAsync(model, RoleConstants.StockTaker);
                    }

                    await _context.SaveChangesAsync();
                    _logger.LogInformation("User Created a new account with password");
                  
                    return RedirectToAction("Users", "Admin");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        public IActionResult EditUser([FromRoute] string Id)
        {
            var user = _context.Users.Where(Users => Users.Id == Id && Users.Archived==false).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }
   
            var _model = new EditUserViewModel
            {
                Id = Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhysicalAddress = user.PhysicalAddress,
                Password = user.Password,
                ConfirmPassword = user.ConfirmPassword,
                CreatedOn = user.CreatedOn,
                Gender = user.Gender,
                Race = user.Race,
                UserRole = user.UserRole,
                Archived = user.Archived,
                Email= user.Email,
                
            };
            return View(_model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> EditUser(EditUserViewModel model)
        {
            model.UserName = model.Email;
            if (ModelState.IsValid)
            {
                var user = _context.Users.Where(Users => Users.Id==model.Id).FirstOrDefault();

                if (user == null)
                {
                    return NotFound();
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhysicalAddress = model.PhysicalAddress;
                user.Password = model.Password;
                user.Email = model.Email;
                user.Archived = model.Archived;
                user.Password= model.Password;
                user.Race = model.Race;
                user.UserRole = model.UserRole;
                user.Gender = model.Gender;
                user.CreatedOn = model.CreatedOn;


                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Users", "Admin");


            }

            return View(model);
        }



    }
}
