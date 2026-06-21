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

        // GET: TICKETS
        public async Task<IActionResult> Index()
        {
            var userName = User.Identity?.Name;

            var query = from ticket in _context.Tickets
                        select ticket;

            var userTickets = query.Where(ticket => ticket.UserName == userName);

            return View(await userTickets.ToListAsync());
        }

        // GET: TICKETS/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TICKETS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Status,Vin,IncidentDescription")] TicketModel ticketmodel)
        {
            ModelState.Remove("UserName");
            ModelState.Remove("Status");

            ticketmodel.UserName = User.Identity!.Name!; // we know this endpoint is locked behind [Authorize].
            ticketmodel.Status = TicketStatus.Submitted;

            if (ModelState.IsValid)
            {
                _context.Add(ticketmodel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(ticketmodel);
        }

    }
}
