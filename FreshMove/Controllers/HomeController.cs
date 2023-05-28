using FreshMove.Constants;
using FreshMove.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FreshMove.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

   
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(RoleConstants.Admin))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if(User.IsInRole(RoleConstants.SalesManager))
                {
                    return RedirectToAction("Index", "SalesManager");

                }
                else if(User.IsInRole(RoleConstants.StockManager))
                {
                    return RedirectToAction("Index", "StockManager");

                }
                else if (User.IsInRole(RoleConstants.StockTaker))
                {
                    return RedirectToAction("Index", "StockTaker");
                }

            }

            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Construction()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}