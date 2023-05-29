using FreshMove.Data;
using FreshMove.Helpers;
using FreshMove.Models.categories;
using FreshMove.Models.products;
using FreshMove.Models.users;
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

        public async Task<IActionResult> Products(string sortOrder, string currentFilter, string searchString, int? pageNumber) 
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "Name";
            ViewData["CategorySortParm"] = String.IsNullOrEmpty(sortOrder) ? "category": "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var products = from p in _context.Products.OrderBy(p=>p.Name).Include(p=>p.Category)
                           where p.Archive == false
                           select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p=>p.Name.Contains(searchString) || p.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(_u => _u.Name);
                    break;
                case "category":
                    products= products.OrderByDescending(_u => _u.Category.Name);
                    break;
                default:
                    products = products.OrderBy(_u => _u.Name);
                    break;

            }
            int pageSize = 4;
            return View(await PaginatedList<Product>.CreateAsync(products.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        
        public IActionResult AddProduct()
        {
            ViewBag.Categories= _context.Categories.Where(c=>c.Archived==false).OrderBy(c => c.Name).ToList();
            ViewBag.Supplier= _context.Suppliers.Where(s=>s.Archived==false).OrderBy(s=>s.Name).ToList();

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
            if (product == null)
            {

                return NotFound();
            }

            ViewBag.Categories = _context.Categories.Where(c => c.Archived == false).OrderBy(c => c.Name).ToList();
            ViewBag.Supplier = _context.Suppliers.Where(s => s.Archived == false).OrderBy(s => s.Name).ToList();

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

        [HttpGet]
        public IActionResult DeleteProduct([FromRoute] string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var product = _context.Products.Where(p => p.Id == Id).FirstOrDefault();
            if (product == null)
            {

                return NotFound();
            }


            return View(product);
        }

        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> confirmDelete(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var product = _context.Products.Where(p => p.Id == Id).FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }
            else
            {

                product.Archive = true;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return RedirectToAction("Products");

            }
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
