using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models.CMS;

namespace Website.Controllers
{
    public class TapController : Controller
    {
        private readonly WebsiteContext _context;

        public TapController(WebsiteContext context)
        {
            _context = context;
        }

        // GET: Tap
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tap.ToListAsync());
        }

        // GET: Tap/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tap = await _context.Tap
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tap == null)
            {
                return NotFound();
            }

            return View(tap);
        }

        // GET: Tap/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Length,Dimension,Material,PassThrough")] Tap tap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tap);
        }

        // GET: Tap/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tap = await _context.Tap.FindAsync(id);
            if (tap == null)
            {
                return NotFound();
            }
            return View(tap);
        }

        // POST: Tap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Length,Dimension,Material,PassThrough")] Tap tap)
        {
            if (id != tap.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TapExists(tap.Id))
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
            return View(tap);
        }

        // GET: Tap/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tap = await _context.Tap
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tap == null)
            {
                return NotFound();
            }

            return View(tap);
        }

        // POST: Tap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tap = await _context.Tap.FindAsync(id);
            if (tap != null)
            {
                _context.Tap.Remove(tap);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TapExists(int id)
        {
            return _context.Tap.Any(e => e.Id == id);
        }
    }
}
