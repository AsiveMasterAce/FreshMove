using FreshMove.Constants;
using FreshMove.Data;
using FreshMove.Models.users;
using FreshMove.Models.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FreshMove.Models.users;
using System.Drawing.Printing;

namespace FreshMove.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RegisterController> _logger;
        private readonly IConfiguration _configuration;

        public RegisterController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context, ILogger<RegisterController> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Admin()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]

        public async Task<IActionResult> Admin(RegisterAdminViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,


                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleConstants.Admin);


                    var Admin = new AdminUser
                    {
                        UserID = user.Id,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Password = model.Password,
                        Gender = model.Gender,
                        Race= model.Race,
                        PhysicalAddress = model.PhysicalAddress,
                        CreatedOn= DateTime.Now,
                        Archived=false,
                       

                    };
                    _context.Add(Admin);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("User Created a new account with password");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Admin");

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
