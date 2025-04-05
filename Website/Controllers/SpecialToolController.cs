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
    public class SpecialToolController : Controller
    {
        private readonly CutItUpContext _context;

        public SpecialToolController(CutItUpContext context)
        {
            _context = context;
        }

        // GET: SpecialTool
        public async Task<IActionResult> Index()
        {
            return View(await _context.SpecialTool.ToListAsync());
        }

        // GET: SpecialTool/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialTools = await _context.SpecialTool
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialTools == null)
            {
                return NotFound();
            }

            return View(specialTools);
        }

        // GET: SpecialTool/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SpecialTool/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,NoCuttingEddges,Price,Dimension,Material,Angle,Radius")] SpecialTool specialTools)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specialTools);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTools);
        }

        // GET: SpecialTool/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialTools = await _context.SpecialTool.FindAsync(id);
            if (specialTools == null)
            {
                return NotFound();
            }
            return View(specialTools);
        }

        // POST: SpecialTool/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,NoCuttingEddges,Price,Dimension,Material,Angle,Radius")] SpecialTool specialTools)
        {
            if (id != specialTools.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specialTools);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialToolsExists(specialTools.Id))
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
            return View(specialTools);
        }

        // GET: SpecialTool/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialTools = await _context.SpecialTool   
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialTools == null)
            {
                return NotFound();
            }

            return View(specialTools);
        }

        // POST: SpecialTool/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialTools = await _context.SpecialTool.FindAsync(id);
            if (specialTools != null)
            {
                _context.SpecialTool.Remove(specialTools);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialToolsExists(int id)
        {
            return _context.SpecialTool.Any(e => e.Id == id);
        }
    }
}
