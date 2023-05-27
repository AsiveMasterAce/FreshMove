﻿using FreshMove.Data;
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

            var cart = _context.Cart.Include(c => c.CartItems).FirstOrDefault(c => c.CustomerID == customer.Id);
            
            if (cart == null)
            {
                return View(new CartViewModel());
            }

            

            return View();
        }
        
    }
}
