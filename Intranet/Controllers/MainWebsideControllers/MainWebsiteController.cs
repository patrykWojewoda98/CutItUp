using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CutItUp.Data.Context;
using CutItUp.Data.Data.CMS.MainWebsite;

namespace Intranet.Controllers.MainWebsideControllers
{
    public class MainWebsiteController : Controller
    {
        private readonly CutItUpContext _context;

        public MainWebsiteController(CutItUpContext context)
        {
            _context = context;
        }

        // GET: MainWebsite
        public async Task<IActionResult> Index()
        {
            return View(await _context.MainWebsite.ToListAsync());
        }

        // GET: MainWebsite/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainWebsite = await _context.MainWebsite
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mainWebsite == null)
            {
                return NotFound();
            }

            return View(mainWebsite);
        }

        // GET: MainWebsite/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MainWebsite/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] MainWebsite mainWebsite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mainWebsite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mainWebsite);
        }

        // GET: MainWebsite/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainWebsite = await _context.MainWebsite.FindAsync(id);
            if (mainWebsite == null)
            {
                return NotFound();
            }
            return View(mainWebsite);
        }

        // POST: MainWebsite/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] MainWebsite mainWebsite)
        {
            if (id != mainWebsite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mainWebsite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MainWebsiteExists(mainWebsite.Id))
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
            return View(mainWebsite);
        }

        // GET: MainWebsite/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mainWebsite = await _context.MainWebsite
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mainWebsite == null)
            {
                return NotFound();
            }

            return View(mainWebsite);
        }

        // POST: MainWebsite/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mainWebsite = await _context.MainWebsite.FindAsync(id);
            if (mainWebsite != null)
            {
                _context.MainWebsite.Remove(mainWebsite);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MainWebsiteExists(int id)
        {
            return _context.MainWebsite.Any(e => e.Id == id);
        }
    }
}
