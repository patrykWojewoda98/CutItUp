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
    public class WhyUsSectionController : Controller
    {
        private readonly CutItUpContext _context;

        public WhyUsSectionController(CutItUpContext context)
        {
            _context = context;
        }

        // GET: WhyUsSection
        public async Task<IActionResult> Index()
        {
            return View(await _context.WhyUsSection.ToListAsync());
        }

        // GET: WhyUsSection/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var whyUsSection = await _context.WhyUsSection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (whyUsSection == null)
            {
                return NotFound();
            }

            return View(whyUsSection);
        }

        // GET: WhyUsSection/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WhyUsSection/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Reasons")] WhyUsSection whyUsSection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(whyUsSection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(whyUsSection);
        }

        // GET: WhyUsSection/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var whyUsSection = await _context.WhyUsSection.FindAsync(id);
            if (whyUsSection == null)
            {
                return NotFound();
            }
            return View(whyUsSection);
        }

        // POST: WhyUsSection/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Reasons")] WhyUsSection whyUsSection)
        {
            if (id != whyUsSection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(whyUsSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WhyUsSectionExists(whyUsSection.Id))
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
            return View(whyUsSection);
        }

        // GET: WhyUsSection/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var whyUsSection = await _context.WhyUsSection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (whyUsSection == null)
            {
                return NotFound();
            }

            return View(whyUsSection);
        }

        // POST: WhyUsSection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var whyUsSection = await _context.WhyUsSection.FindAsync(id);
            if (whyUsSection != null)
            {
                _context.WhyUsSection.Remove(whyUsSection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WhyUsSectionExists(int id)
        {
            return _context.WhyUsSection.Any(e => e.Id == id);
        }
    }
}
