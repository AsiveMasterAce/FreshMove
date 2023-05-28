using FreshMove.Data;
using FreshMove.Models.users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreshMove.Models.Cart;

namespace FreshMove.Controllers
{
    public class CartController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
     
        private readonly ApplicationDbContext _context;
        public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) 
        { 
            _userManager= userManager;
            _context= context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cart()
        {
            var customer = _context.Customers.Where(c => c.UserID == _userManager.GetUserId(User)).FirstOrDefault();

            var cart = _context.Cart.Include(c => c.CartItems).ThenInclude(c=>c.Product).FirstOrDefault(c => c.CustomerID == customer.Id);
            var cartItem = _context.CartItems.Where(cI=>cI.CartID==cart.Id).ToList();
            
            if (cart == null)
            {
                cart = new Cart
                {
                    CustomerID = customer.Id,
                };
                _context.Cart.Add(cart);
                _context.SaveChanges();
                   

            }
            var cartItems = cart.CartItems.Select(ci => new CartItemViewModel
            {
                ProductID = ci.ProductID,
                productImage = ci.Product.productImage,
                productName = ci.Product.Name,
                TotalPrice = ci.TotalPrice,
                Quantity = ci.Quantity,
            }).ToHashSet();

            var viewModel = new CartViewModel
            {
               CartItems=cartItems,
               CartTotal=cartItems.Sum(ci=>ci.TotalPrice * ci.Quantity),

            };


            return View(viewModel);
        }
        
    }
}
