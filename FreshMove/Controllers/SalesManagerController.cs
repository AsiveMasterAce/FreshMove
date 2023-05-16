using FreshMove.Constants;
using FreshMove.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Drawing.Printing;

namespace FreshMove.Controllers
{
    [Authorize(Roles = RoleConstants.SalesManager)]
    public class SalesManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SalesManagerController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListCustomer()
        {
            var customers = _context.Customers.Where(c=>c.Archived==false).ToList();
            return View(customers);
        }
        
    }
}
