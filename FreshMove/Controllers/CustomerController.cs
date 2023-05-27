using FreshMove.Data;
using FreshMove.Models.users;
using FreshMove.Models.Cart;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshMove.Controllers
{

    public class CustomerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public CustomerController(ApplicationDbContext context,SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) 
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cart()
        {
            var cart = _context.Cart.Where(c => c.CustomerID == _userManager.GetUserId(User)).ToList();

            return View(cart);
        }

        //public async Task<ActionResult> AddToBag(string prouductID, int Quantity)
        //{
        //    var customer= _context.Customers.Where(c=>c.UserID==_userManager.GetUserId(User)).FirstOrDefault();

        //    var bagItem= _context.Cart;

        //    if (bagItem!=null)
        //    {
        //        bagItem.quantity += Quantity;

        //    }
        //    else
        //    {

        //        bagItem = new Cart
        //        {
        //            ProductID = prouductID,
        //            quantity = Quantity,
        //            CustomerID = customer.UserID
                   
        //        };
        //        _context.Add(bagItem);
        //    }

            
        //    await _context.SaveChangesAsync();

        //    return View();
        //}
        
    }
}
