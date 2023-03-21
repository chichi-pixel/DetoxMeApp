using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DetoxMeApp.Data;
using DetoxMeApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace DetoxMeApp.Controllers
{
    public class DetoxController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetoxController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Detox
        public async Task<IActionResult> Index()
        {
            return View(await _context.Detox.ToListAsync());
        }

        // GET: Detox/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: Detox/ShowSearchResults
        public async Task<IActionResult>ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Detox.Where( j=> j.DetoxQuestion.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Detox/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detox = await _context.Detox
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detox == null)
            {
                return NotFound();
            }

            return View(detox);
        }

        // GET: Detox/Create

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Detox/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DetoxQuestion,DetoxAnswer")] Detox detox)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detox);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(detox);
        }

        // GET: Detox/Edit/5

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detox = await _context.Detox.FindAsync(id);
            if (detox == null)
            {
                return NotFound();
            }
            return View(detox);
        }

        // POST: Detox/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DetoxQuestion,DetoxAnswer")] Detox detox)
        {
            if (id != detox.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detox);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetoxExists(detox.Id))
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
            return View(detox);
        }

        // GET: Detox/Delete/5

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detox = await _context.Detox
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detox == null)
            {
                return NotFound();
            }

            return View(detox);
        }

        // POST: Detox/Delete/5

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detox = await _context.Detox.FindAsync(id);
            _context.Detox.Remove(detox);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetoxExists(int id)
        {
            return _context.Detox.Any(e => e.Id == id);
        }
    }
}
