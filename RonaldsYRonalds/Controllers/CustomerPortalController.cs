using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RonaldsYRonalds.Models;
using RonaldsYRonalds.Data;

namespace RonaldsYRonalds.Controllers
{

    [Authorize]
    public class CustomerPortalController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IActionResult> Index()
        {
            var userName = User.Identity?.Name;

            var query = from ticket in _context.Tickets
                        select ticket;

            var userTickets = query.Where(ticket => ticket.UserName == userName);

            return View(await userTickets.ToListAsync());
        }
    }
}
