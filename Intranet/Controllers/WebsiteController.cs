using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CutItUp.Data.Context;
using CutItUp.Data.Data.CMS;
using System.Diagnostics;

namespace Intranet.Controllers
{
    public class WebsiteController : Controller
    {
        private readonly CutItUpContext _context;

        public WebsiteController(CutItUpContext context)
        {
            _context = context;
        }

        // GET: Website
        public async Task<IActionResult> Index()
        {
            return View(await _context.Website.ToListAsync());
        }

        // GET: Website/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var website = await _context.Website
                .FirstOrDefaultAsync(m => m.Id == id);
            if (website == null)
            {
                return NotFound();
            }

            return View(website);
        }

        // GET: Website/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Website/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Website website)
        {
            Debug.WriteLine("🔥 Create(Website) działa!");

            if (!ModelState.IsValid)
            {
                Debug.WriteLine("❌ ModelState NIE jest poprawny!");
                foreach (var kvp in ModelState)
                {
                    if (kvp.Value.Errors.Any())
                    {
                        Debug.WriteLine($"❗ Błąd dla pola '{kvp.Key}': {string.Join(", ", kvp.Value.Errors.Select(e => e.ErrorMessage))}");
                    }
                }
                return View(website);
            }

            Debug.WriteLine("✅ ModelState.IsValid");

            Debug.WriteLine($"📄 Tytuł strony: {website.Title}");

            Debug.WriteLine($"🧩 TitleDecriptionParts.Count: {website.TitleDecriptionParts?.Count}");
            if (website.TitleDecriptionParts != null)
            {
                int i = 0;
                foreach (var part in website.TitleDecriptionParts)
                {
                    Debug.WriteLine($"  • [{i}] Tytuł: {part.Title}, Opis: {part.Description}");
                    i++;
                }
            }

            Debug.WriteLine($"📋 ListParts.Count: {website.ListParts?.Count}");
            if (website.ListParts != null)
            {
                int i = 0;
                foreach (var list in website.ListParts)
                {
                    Debug.WriteLine($"  • [{i}] Tytuł: {list.Title}, Opis: {list.Description}");
                    if (list.ListContent != null)
                    {
                        for (int j = 0; j < list.ListContent.Count; j++)
                        {
                            Debug.WriteLine($"     - Element[{j}]: {list.ListContent[j]}");
                        }
                    }
                    i++;
                }
            }

            if (website.TitleDecriptionParts != null)
            {
                foreach (var part in website.TitleDecriptionParts)
                {
                    part.Website = website;
                }
            }

            if (website.ListParts != null)
            {
                foreach (var list in website.ListParts)
                {
                    list.Website = website;
                }
            }

            _context.Add(website);
            await _context.SaveChangesAsync();

            Debug.WriteLine("✅ Zapisano do bazy danych.");
            return RedirectToAction(nameof(Index));
        }

        // GET: Website/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();



            // ✅ Poprawnie:
            var website = await _context.Website
                .Include(w => w.TitleDecriptionParts)
                .Include(w => w.ListParts)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (website == null)
                return NotFound();

            return View(website);
        }

        // POST: Website/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Website website)
        {
            if (id != website.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(website);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebsiteExists(website.Id))
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
            return View(website);
        }

        // GET: Website/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var website = await _context.Website
                .FirstOrDefaultAsync(m => m.Id == id);
            if (website == null)
            {
                return NotFound();
            }

            return View(website);
        }

        // POST: Website/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var website = await _context.Website.FindAsync(id);
            if (website != null)
            {
                _context.Website.Remove(website);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebsiteExists(int id)
        {
            return _context.Website.Any(e => e.Id == id);
        }
    }
}