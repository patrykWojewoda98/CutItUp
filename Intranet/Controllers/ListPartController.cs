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
    public class ListPartController : Controller
    {
        private readonly CutItUpContext _context;

        public ListPartController(CutItUpContext context)
        {
            _context = context;
        }

        // GET: ListPart
        public async Task<IActionResult> Index()
        {
            var cutItUpContext = _context.ListPart.Include(l => l.Website);
            return View(await cutItUpContext.ToListAsync());
        }

        // GET: ListPart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listPart = await _context.ListPart
                .Include(l => l.Website)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listPart == null)
            {
                return NotFound();
            }

            return View(listPart);
        }

        // GET: ListPart/Create
        public IActionResult Create()
        {
            ViewData["WebsiteId"] = new SelectList(_context.Set<Website>(), "Id", "Title");
            return View();
        }

        // POST: ListPart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ListContent,ImageUrl,WebsiteId")] ListPart listPart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listPart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WebsiteId"] = new SelectList(_context.Set<Website>(), "Id", "Title", listPart.WebsiteId);
            return View(listPart);
        }

        // GET: ListPart/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listPart = await _context.ListPart.FindAsync(id);
            if (listPart == null)
            {
                return NotFound();
            }
            ViewData["WebsiteId"] = new SelectList(_context.Set<Website>(), "Id", "Title", listPart.WebsiteId);
            return View(listPart);
        }

        // POST: ListPart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ListContent,ImageUrl,WebsiteId")] ListPart listPart)
        {
            if (id != listPart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listPart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListPartExists(listPart.Id))
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
            ViewData["WebsiteId"] = new SelectList(_context.Set<Website>(), "Id", "Title", listPart.WebsiteId);
            return View(listPart);
        }

        // GET: ListPart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listPart = await _context.ListPart
                .Include(l => l.Website)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listPart == null)
            {
                return NotFound();
            }

            return View(listPart);
        }

        // POST: ListPart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listPart = await _context.ListPart.FindAsync(id);
            if (listPart != null)
            {
                _context.ListPart.Remove(listPart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListPartExists(int id)
        {
            return _context.ListPart.Any(e => e.Id == id);
        }
    }
}
