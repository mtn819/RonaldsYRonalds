using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RonaldsYRonalds.Controllers
{
    public class CustomerPortalController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
