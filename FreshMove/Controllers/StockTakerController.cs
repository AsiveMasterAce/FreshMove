using FreshMove.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FreshMove.Controllers
{
    [Authorize(Roles = RoleConstants.StockTaker)]
    [Route("StockTaker")]
    public class StockTakerController : Controller
    {
        [Route("StockTaker/Index")]
        public IActionResult Index()
        {

            return View();
        }
    }
}
