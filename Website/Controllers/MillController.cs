using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CutItUp.Data.Context;
using CutItUp.Data.Data.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace Website.Controllers
{
    public class MillController : Controller
    {
        private readonly CutItUpContext _context;

        public MillController(CutItUpContext context)
        {
            _context = context;
        }

        // GET: Mill
        public async Task<IActionResult> Index()
        {
            return View(await _context.Mill.ToListAsync());
        }

        // GET: Mill/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mill = await _context.Mill
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mill == null)
            {
                return NotFound();
            }

            return View(mill);
        }

        // GET: Mill/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mill/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,NoCuttingEddges,Price,Dimension,Material")] Mill mill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mill);
        }

        // GET: Mill/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mill = await _context.Mill.FindAsync(id);
            if (mill == null)
            {
                return NotFound();
            }
            return View(mill);
        }

        // POST: Mill/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,NoCuttingEddges,Price,Dimension,Material")] Mill mill)
        {
            if (id != mill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MillExists(mill.Id))
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
            return View(mill);
        }

        // GET: Mill/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mill = await _context.Mill
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mill == null)
            {
                return NotFound();
            }

            return View(mill);
        }

        // POST: Mill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mill = await _context.Mill.FindAsync(id);
            if (mill != null)
            {
                _context.Mill.Remove(mill);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MillExists(int id)
        {
            return _context.Mill.Any(e => e.Id == id);
        }
    }
}
