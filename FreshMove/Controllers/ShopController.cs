
using FreshMove.Data;
using FreshMove.Models.categories;
using FreshMove.Models.products;
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
     
        public async Task<IActionResult> Shop(string category, string searchString)
        {

            var products = _context.Products.Where(p => p.Archive == false);

            switch (category)
            {
                case "vegetable":
                    products = products.Where(p => p.Category.Name == category);
                    break;
                case "fruit":
                    products = products.Where(p => p.Category.Name == category);
                    break;
                case "frozenGoods":
                    products = products.Where(p => p.Category.Name == category);
                    break;
                case "meat":
                    products = products.Where(p => p.Category.Name == category);
                    break;
                case "all":
                    products = products;
                    break;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }

            

            ViewBag.Products = products.ToList();
            return View();
        }
       

    }
}
