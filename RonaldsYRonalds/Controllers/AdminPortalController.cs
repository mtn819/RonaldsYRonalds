using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace RonaldsYRonalds.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPortalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
