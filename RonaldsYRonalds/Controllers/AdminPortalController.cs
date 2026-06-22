using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RonaldsYRonalds.Data;
using RonaldsYRonalds.Models;

namespace RonaldsYRonalds.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPortalController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        // GET: AdminPortal
        public async Task<IActionResult> Index(String searchVin)
        {
            var query = from ticket in _context.Tickets
                        select ticket;

            
            if (!String.IsNullOrEmpty(searchVin))
            {
                query = query.Where(ticket => ticket.Vin == searchVin);
            }

            query = query.OrderByDescending(ticket => ticket.CreatedAt);

            return View(await query.ToListAsync());
        }

        // GET: AdminPortal/Process/5
        public async Task<IActionResult> Process(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketmodel = await _context.Tickets.FindAsync(id);
            if (ticketmodel == null)
            {
                return NotFound();
            }
            return View(ticketmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Fulfill(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
                return NotFound();

            ticket.Status = TicketStatus.Fulfilled;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
                return NotFound();

            ticket.Status = TicketStatus.Rejected;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
