
using FreshMove.Data;
using FreshMove.Helpers;
using FreshMove.Models.categories;
using FreshMove.Models.products;
using FreshMove.Models.ViewModels.Shop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Shop(string category, string searchString,int pageIndex = 1, int pageSize = 8)
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
                case "frozen goods":
                    products = products.Where(p => p.Category.Name == category);
                    break;
                case "meat":
                    products = products.Where(p => p.Category.Name == category);
                    break;
                case "bakery":
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


            var paginatedProducts = await PaginatedList<Product>.CreateAsync(products, pageIndex, pageSize);
            ViewBag.Products = paginatedProducts;
            return View();
        }
       

    }
}
