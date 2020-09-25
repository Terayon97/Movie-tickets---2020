using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektAplikacjeInternetowe.Data;
using ProjektAplikacjeInternetowe.Models;

namespace ProjektAplikacjeInternetowe.Controllers.CRUD
{
    [Authorize(Roles = "Admin")]
    public class _MovieDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public _MovieDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: _MovieDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.MovieDetails.ToListAsync());
        }


        // GET: _MovieDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: _MovieDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Movie_Name,Movie_Description,DateToPresent,MoviePicture")] MovieDetails movieDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movieDetails);
        }

        // GET: _MovieDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieDetails = await _context.MovieDetails.FindAsync(id);
            if (movieDetails == null)
            {
                return NotFound();
            }
            return View(movieDetails);
        }

        // POST: _MovieDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Movie_Name,Movie_Description,DateToPresent,MoviePicture")] MovieDetails movieDetails)
        {
            if (id != movieDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieDetailsExists(movieDetails.Id))
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
            return View(movieDetails);
        }

        // GET: _MovieDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieDetails = await _context.MovieDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieDetails == null)
            {
                return NotFound();
            }

            return View(movieDetails);
        }

        // POST: _MovieDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieDetails = await _context.MovieDetails.FindAsync(id);
            _context.MovieDetails.Remove(movieDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieDetailsExists(int id)
        {
            return _context.MovieDetails.Any(e => e.Id == id);
        }
    }
}
