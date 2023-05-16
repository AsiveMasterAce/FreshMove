using FreshMove.Constants;
using FreshMove.Data;
using FreshMove.Models.users;
using FreshMove.Models.ViewModels.Admin;
using FreshMove.Models.ViewModels.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreshMove.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context, ILogger<RegisterController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Admin()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Admin(ApplicationUser model)
        {
            model.UserName = model.Email;
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(model, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(model, RoleConstants.Admin);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("User Created a new account with password");
                    await _signInManager.SignInAsync(model, isPersistent: false);
                    return RedirectToAction("Index", "Admin");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }

            return View(model);

        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Customer()
        {

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Customer(RegisterCustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Race = model.Race,
                    Gender = model.Gender,
                    PhysicalAddress = model.PhysicalAddress,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword,
                    UserRole = UserRole.Customer,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleConstants.Customer);

                    var customer = new Customer
                    {

                        UserID = user.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PhysicalAddress = model.PhysicalAddress,
                        Gender= model.Gender,
                        Race=model.Race,
                        CreatedOn= model.CreatedOn,
                        Archived=false,
                        Email=model.Email,
                        Password=model.Password,


                    };
                    _context.Customers.Add(customer);
                    await _context.SaveChangesAsync();
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User Created a new account with password");
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            return View(model);
            
        }





    }
}
