using FreshMove.Constants;
using FreshMove.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FreshMove.Controllers
{
    [Authorize(Roles = RoleConstants.StockTaker)]
    [Route("StockTaker")]
    public class StockTakerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StockTakerController(ApplicationDbContext context) 
        {
            _context = context;        
        }

        [Route("StockTaker/Index")]
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Categories()
        {
            var categories = _context.Categories.OrderBy(c => c.Name).ToList();

            return View(categories);

        }
    }
}
