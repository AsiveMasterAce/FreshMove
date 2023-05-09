using FreshMove.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshMove.Controllers
{
    [Authorize(Roles = RoleConstants.StockManager)]
    public class StockManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
