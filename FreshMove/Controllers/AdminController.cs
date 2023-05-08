using FreshMove.Constants;
using FreshMove.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FreshMove.Controllers
{
    [Authorize(Roles = RoleConstants.Admin)]
    
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult newUser()
        {

            return View();
        }

        public async Task<ActionResult> newUser(newUserViewModel model)
        {



            return View();
        }



    }
}
