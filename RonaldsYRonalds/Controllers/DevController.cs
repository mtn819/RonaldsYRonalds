using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RonaldsYRonalds.Controllers
{
    public class DevController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) : Controller
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        [HttpGet]
        public async Task<IActionResult> MakeAdmin(string username)
        {
            // create role
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            // pick user
            var user = await userManager.FindByEmailAsync(username);

            if (user != null)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }

            return Ok($"{username} is admin");
        }
    }
}
