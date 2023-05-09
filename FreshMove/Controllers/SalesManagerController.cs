using FreshMove.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreshMove.Controllers
{
    [Authorize(Roles = RoleConstants.SalesManager)]
    public class SalesManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
