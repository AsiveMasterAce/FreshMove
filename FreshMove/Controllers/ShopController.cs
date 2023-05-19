
using FreshMove.Data;
using FreshMove.Models.ViewModels.Shop;
using Microsoft.AspNetCore.Mvc;

namespace FreshMove.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ShopController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
     
        public IActionResult Shop()
        {

            var products = _context.Products.Where(p => p.Archive == false).ToList();

            var viewModel = new ShopViewModel
            {
                products = products
            };
            return View(viewModel);
        }
    }
}
