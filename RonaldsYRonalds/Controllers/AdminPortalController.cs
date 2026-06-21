using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RonaldsYRonalds.Data;

namespace RonaldsYRonalds.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPortalController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        // GET: TICKETS
        public async Task<IActionResult> Index()
        {
            var userName = User.Identity?.Name;

            var query = from ticket in _context.Tickets
                        select ticket;

            var userTickets = query.OrderByDescending(ticket => ticket.CreatedAt);

            return View(await userTickets.ToListAsync());
        }
    }
}
