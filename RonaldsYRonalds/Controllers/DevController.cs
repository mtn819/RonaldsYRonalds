using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RonaldsYRonalds.Controllers
{
    public class DevController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public DevController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> MakeAdmin(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return Content("Done");
        }
    }
}
