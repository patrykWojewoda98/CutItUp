using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CutItUp.Data.Context;
using CutItUp.Data.Data.CMS;

namespace Intranet.Controllers
{
    public class TitleDescriptionPartController : Controller
    {
        private readonly CutItUpContext _context;

        public TitleDescriptionPartController(CutItUpContext context)
        {
            _context = context;
        }

        // GET: TitleDescriptionPart
        public async Task<IActionResult> Index()
        {
            var cutItUpContext = _context.TitleDescriptionPart.Include(t => t.Website);
            return View(await cutItUpContext.ToListAsync());
        }

        // GET: TitleDescriptionPart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var titleDescriptionPart = await _context.TitleDescriptionPart
                .Include(t => t.Website)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (titleDescriptionPart == null)
            {
                return NotFound();
            }

            return View(titleDescriptionPart);
        }

        // GET: TitleDescriptionPart/Create
        public IActionResult Create()
        {
            ViewData["WebsiteId"] = new SelectList(_context.Set<Website>(), "Id", "Title");
            return View();
        }

        // POST: TitleDescriptionPart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ImageUrl,WebsiteId")] TitleDescriptionPart titleDescriptionPart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(titleDescriptionPart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WebsiteId"] = new SelectList(_context.Set<Website>(), "Id", "Title", titleDescriptionPart.WebsiteId);
            return View(titleDescriptionPart);
        }

        // GET: TitleDescriptionPart/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var titleDescriptionPart = await _context.TitleDescriptionPart.FindAsync(id);
            if (titleDescriptionPart == null)
            {
                return NotFound();
            }
            ViewData["WebsiteId"] = new SelectList(_context.Set<Website>(), "Id", "Title", titleDescriptionPart.WebsiteId);
            return View(titleDescriptionPart);
        }

        // POST: TitleDescriptionPart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ImageUrl,WebsiteId")] TitleDescriptionPart titleDescriptionPart)
        {
            if (id != titleDescriptionPart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(titleDescriptionPart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TitleDescriptionPartExists(titleDescriptionPart.Id))
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
            ViewData["WebsiteId"] = new SelectList(_context.Set<Website>(), "Id", "Title", titleDescriptionPart.WebsiteId);
            return View(titleDescriptionPart);
        }

        // GET: TitleDescriptionPart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var titleDescriptionPart = await _context.TitleDescriptionPart
                .Include(t => t.Website)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (titleDescriptionPart == null)
            {
                return NotFound();
            }

            return View(titleDescriptionPart);
        }

        // POST: TitleDescriptionPart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var titleDescriptionPart = await _context.TitleDescriptionPart.FindAsync(id);
            if (titleDescriptionPart != null)
            {
                _context.TitleDescriptionPart.Remove(titleDescriptionPart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TitleDescriptionPartExists(int id)
        {
            return _context.TitleDescriptionPart.Any(e => e.Id == id);
        }
    }
}
