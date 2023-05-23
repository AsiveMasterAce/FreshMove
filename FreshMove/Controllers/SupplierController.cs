using FreshMove.Data;
using FreshMove.Models.actors;
using FreshMove.Models.users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshMove.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SupplierController(ApplicationDbContext context)
        {

            _context = context;
          
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Suppliers()
        {
            var supplier=_context.Suppliers.OrderBy(s=>s.Name).ToList();
            return View(supplier);
        }


        public IActionResult AddSupplier()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddSupplier(Supplier model)
        {

            if(ModelState.IsValid)
            {
                _context.Suppliers.Add(model);
               await _context.SaveChangesAsync();
               return RedirectToAction("Suppliers");
            }


            return View(model);

        }
        public IActionResult EditSupplier([FromRoute] string Id)
        {
            var supplier= _context.Suppliers.Where(s=>s.Id==Id && s.Archived==false).FirstOrDefault();

            if(supplier==null)
            {

                return NotFound();
            }

            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditSupplier(Supplier model)
        {

            if (ModelState.IsValid)
            {

                var supplier = _context.Suppliers.Where(s => s.Id == model.Id).FirstOrDefault();

                if(supplier==null)
                {
                    return NotFound();

                }
                supplier.Archived = model.Archived;
                supplier.Name= model.Name;
                supplier.Email= model.Email;
                supplier.PhysicalAddress= model.PhysicalAddress;

                _context.Suppliers.Update(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction("Suppliers");
            }

            return View(model);

        }



    }
}
