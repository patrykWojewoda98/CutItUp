using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CutItUp.Data.Context;
using CutItUp.Data.Data.Tools;

namespace Intranet.Controllers
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

            var specialTool = await _context.SpecialTool
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialTool == null)
            {
                return NotFound();
            }

            return View(specialTool);
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
        public async Task<IActionResult> Create([Bind("NoCuttingEddges,Angle,Radius,Id,Name,Description,Price,Dimension,Material,Length,NoOfToolsInMagazine,ImageUrl,NoOfSaled")] SpecialTool specialTool)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specialTool);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTool);
        }

        // GET: SpecialTool/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialTool = await _context.SpecialTool.FindAsync(id);
            if (specialTool == null)
            {
                return NotFound();
            }
            return View(specialTool);
        }

        // POST: SpecialTool/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoCuttingEddges,Angle,Radius,Id,Name,Description,Price,Dimension,Material,Length,NoOfToolsInMagazine,ImageUrl,NoOfSaled")] SpecialTool specialTool)
        {
            if (id != specialTool.Id)
            {
                return NotFound();
            }


            ModelState.Remove("ImageFile");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specialTool);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialToolExists(specialTool.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Tool");
            }
            return View(specialTool);
        }

        // GET: SpecialTool/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialTool = await _context.SpecialTool
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialTool == null)
            {
                return NotFound();
            }

            return View(specialTool);
        }

        // POST: SpecialTool/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialTool = await _context.SpecialTool.FindAsync(id);
            if (specialTool != null)
            {
                _context.SpecialTool.Remove(specialTool);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Tool");
        }

        private bool SpecialToolExists(int id)
        {
            return _context.SpecialTool.Any(e => e.Id == id);
        }
    }
}
