using AspNetCore;
using FreshMove.Data;
using FreshMove.Migrations;
using FreshMove.Models.categories;
using FreshMove.Models.products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace FreshMove.Controllers
{
    public class ProductController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment )
        {

            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Products() 
        {
            var products = _context.Products.ToList();
            return View(products);
        }
        
        public IActionResult AddProduct()
        {
           
            
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.Supplier= _context.Suppliers.ToList();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddProduct(AddProductViewModel model)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);

                var product = new Product
                { 

                    Name=model.Name,
                    Description=model.Description,
                    productCode=model.productCode,
                    SupplierID=model.SupplierID,
                    CategoryID=model.CategoryID,
                    Quantity=model.Quantity,
                    Price=model.Price,
                    productImage=uniqueFileName,
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Products");
            }
            return View(model);


        }
        private string ProcessUploadedFile(AddProductViewModel model)
        {
            string uniqueFileName = null;

            if (model.productImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Product");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.productImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.productImage.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        private string ProcessUploadedEditFile(EditCategoryVM model)
        {
            string uniqueFileName = null;

            if (model.categoryImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Product");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.categoryImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.categoryImage.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }




    }
}
