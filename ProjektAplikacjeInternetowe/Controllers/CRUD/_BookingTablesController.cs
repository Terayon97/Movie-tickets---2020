using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektAplikacjeInternetowe.Data;
using ProjektAplikacjeInternetowe.Models;

namespace ProjektAplikacjeInternetowe.Controllers.CRUD
{
    public class _BookingTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public _BookingTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: _BookingTables
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BookingTable.Include(b => b.moviedetails);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: _BookingTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingTable = await _context.BookingTable
                .Include(b => b.moviedetails)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingTable == null)
            {
                return NotFound();
            }

            return View(bookingTable);
        }

        // GET: _BookingTables/Create
        public IActionResult Create()
        {
            ViewData["MovieDetailsId"] = new SelectList(_context.MovieDetails, "Id", "Id");
            return View();
        }

        // POST: _BookingTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,seatNo,UserId,DateToPresent,MovieDetailsId,MovieName,Amount")] BookingTable bookingTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookingTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieDetailsId"] = new SelectList(_context.MovieDetails, "Id", "Id", bookingTable.MovieDetailsId);
            return View(bookingTable);
        }

        // GET: _BookingTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingTable = await _context.BookingTable.FindAsync(id);
            if (bookingTable == null)
            {
                return NotFound();
            }
            ViewData["MovieDetailsId"] = new SelectList(_context.MovieDetails, "Id", "Id", bookingTable.MovieDetailsId);
            return View(bookingTable);
        }

        // POST: _BookingTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,seatNo,UserId,DateToPresent,MovieDetailsId,MovieName,Amount")] BookingTable bookingTable)
        {
            if (id != bookingTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingTableExists(bookingTable.Id))
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
            ViewData["MovieDetailsId"] = new SelectList(_context.MovieDetails, "Id", "Id", bookingTable.MovieDetailsId);
            return View(bookingTable);
        }

        // GET: _BookingTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingTable = await _context.BookingTable
                .Include(b => b.moviedetails)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingTable == null)
            {
                return NotFound();
            }

            return View(bookingTable);
        }

        // POST: _BookingTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookingTable = await _context.BookingTable.FindAsync(id);
            _context.BookingTable.Remove(bookingTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingTableExists(int id)
        {
            return _context.BookingTable.Any(e => e.Id == id);
        }
    }
}
