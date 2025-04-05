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
    public class DrillController : Controller
    {
        private readonly CutItUpContext _context;

        public DrillController(CutItUpContext context)
        {
            _context = context;
        }

        // GET: Drill
        public async Task<IActionResult> Index()
        {
            return View(await _context.Drill.ToListAsync());
        }

        // GET: Drill/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drill = await _context.Drill
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drill == null)
            {
                return NotFound();
            }

            return View(drill);
        }

        // GET: Drill/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Drill/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Angle,Price,Length,Dimension,Material")] Drill drill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(drill);
        }

        // GET: Drill/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drill = await _context.Drill.FindAsync(id);
            if (drill == null)
            {
                return NotFound();
            }
            return View(drill);
        }

        // POST: Drill/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Angle,Price,Length,Dimension,Material")] Drill drill)
        {
            if (id != drill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrillExists(drill.Id))
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
            return View(drill);
        }

        // GET: Drill/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drill = await _context.Drill
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drill == null)
            {
                return NotFound();
            }

            return View(drill);
        }

        // POST: Drill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drill = await _context.Drill.FindAsync(id);
            if (drill != null)
            {
                _context.Drill.Remove(drill);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrillExists(int id)
        {
            return _context.Drill.Any(e => e.Id == id);
        }
    }
}
