
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RonaldsYRonalds.Models;
using RonaldsYRonalds.Data;

public class TicketsController : Controller
{
    private readonly ApplicationDbContext _context;

    public TicketsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: TICKETS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Tickets.ToListAsync());
    }

    // GET: TICKETS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ticketmodel = await _context.Tickets
            .FirstOrDefaultAsync(m => m.Id == id);
        if (ticketmodel == null)
        {
            return NotFound();
        }

        return View(ticketmodel);
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
    public async Task<IActionResult> Create([Bind("Id,UserName,Status,Vin,IncidentDescription")] TicketModel ticketmodel)
    {
        if (ModelState.IsValid)
        {
            _context.Add(ticketmodel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(ticketmodel);
    } 

    // GET: TICKETS/Edit/5
    public async Task<IActionResult> Edit(int? id)
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

    // POST: TICKETS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,UserName,Status,Vin,IncidentDescription")] TicketModel ticketmodel)
    {
        if (id != ticketmodel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(ticketmodel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketModelExists(ticketmodel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(ticketmodel);
    }

    // GET: TICKETS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ticketmodel = await _context.Tickets
            .FirstOrDefaultAsync(m => m.Id == id);
        if (ticketmodel == null)
        {
            return NotFound();
        }

        return View(ticketmodel);
    }

    // POST: TICKETS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var ticketmodel = await _context.Tickets.FindAsync(id);
        if (ticketmodel != null)
        {
            _context.Tickets.Remove(ticketmodel);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TicketModelExists(int? id)
    {
        return _context.Tickets.Any(e => e.Id == id);
    }
}
