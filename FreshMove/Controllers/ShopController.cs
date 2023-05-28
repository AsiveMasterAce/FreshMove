
using FreshMove.Data;
using FreshMove.Helpers;
using FreshMove.Models.Cart;
using FreshMove.Models.categories;
using FreshMove.Models.products;
using FreshMove.Models.users;
using FreshMove.Models.ViewModels.Shop;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace FreshMove.Controllers
{
    public class ShopController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ShopController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager= userManager;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddToCart(AddToCartViewModel model)
        {
            var product = _context.Products.Where(p => p.Id == model.productID).FirstOrDefault();
            var customer = _context.Customers.Where(c => c.UserID == _userManager.GetUserId(User)).FirstOrDefault();

            if (product == null || product==null)
            {
                return NotFound();
            }

            var cart = _context.Cart.FirstOrDefault(c => c.CustomerID == customer.Id);

            if (cart == null)
            {
                cart = new Cart
                {
                    CustomerID=customer.Id,
                  

                };
                _context.Cart.Add(cart);
            }

            var cartItem= cart.CartItems.FirstOrDefault(ci=>ci.ProductID==model.productID);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    CartID = cart.Id,
                    ProductID = product.Id,
                    Quantity = 1,
                    TotalPrice = product.Price
                };
                _context.CartItems.Add(cartItem);

            }
            else
            {
                cartItem.Quantity++;
                cartItem.TotalPrice = product.Price * cartItem.Quantity;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Cart","CartController");
         

        }

    }
}
