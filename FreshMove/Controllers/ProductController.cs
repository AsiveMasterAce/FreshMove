using FreshMove.Data;

using FreshMove.Models.categories;
using FreshMove.Models.products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var products = _context.Products.Include(p=>p.Category).ToList();
            return View(products);
        }
        
        public IActionResult AddProduct()
        {
           
            ViewBag.Categories= _context.Categories.OrderByDescending(c => c.Name).ToList();
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

        public IActionResult EditProduct([FromRoute] string ID)
        {
            var product = _context.Products.Where(p=>p.Id==ID).FirstOrDefault();
            ViewBag.Categories = _context.Categories.OrderByDescending(c => c.Name).ToList();
            ViewBag.Supplier = _context.Suppliers.ToList();

            var editProduct = new EditProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryID = product.CategoryID,
                Quantity = product.Quantity,
                SupplierID = product.SupplierID,
                ExistingImage=product.productImage,
                productCode = product.productCode,
                


            };
            return View(editProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProduct(EditProductViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedEditFile(model);
                var product = _context.Products.Where(p=>p.Id ==model.Id).FirstOrDefault();

                if(product == null)
                {

                    return NotFound();
                }

                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.CategoryID = model.CategoryID;
                product.productCode = model.productCode;
                product.SupplierID = model.SupplierID;
                product.Quantity = model.Quantity;

                if (model.ExistingImage != null)
                {
                    if (product.productImage != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "product", model.ExistingImage);
                        System.IO.File.Delete(filePath);

                    }
                }
                product.productImage = uniqueFileName;


                _context.Products.Update(product);
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
        private string ProcessUploadedEditFile(EditProductViewModel model)
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




    }
}
