using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        // GET: WhyUsSection/EditReason/{reason}/{id}
        public async Task<IActionResult> EditReason(string reason, int id)
        {
            var whyUsSection = await _context.WhyUsSection
                .FirstOrDefaultAsync(w => w.Id == id);

            if (whyUsSection == null)
            {
                return NotFound();
            }

            // Zamiana Reasons na List<string> aby móc użyć IndexOf
            List<string> reasonsList = whyUsSection.Reasons.ToList();

            var existingReason = reasonsList.FirstOrDefault(r => r == reason);
            if (existingReason != null)
            {
                int index = reasonsList.IndexOf(existingReason); // teraz działa, ponieważ mamy List<string>
                reasonsList[index] = reason;  // Aktualizujemy powód
            }

            // Zaktualizowanie Reasons w obiekcie WhyUsSection
            whyUsSection.Reasons = reasonsList;

            _context.Update(whyUsSection);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: WhyUsSection/AddReason
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReason(int id, string newReason)
        {
            var whyUsSection = await _context.WhyUsSection
                .FirstOrDefaultAsync(w => w.Id == id);

            if (whyUsSection == null)
            {
                return NotFound();
            }

            // Dodanie nowego powodu
            whyUsSection.Reasons.Add(newReason);

            _context.Update(whyUsSection);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); // Przekierowanie po dodaniu
        }

        // GET: WhyUsSection/DeleteReason/{reason}/{id}
        public async Task<IActionResult> DeleteReason(string reason, int id)
        {
            var whyUsSection = await _context.WhyUsSection
                .FirstOrDefaultAsync(w => w.Id == id);

            if (whyUsSection == null)
            {
                return NotFound();
            }

            // Usuwanie powodu z kolekcji Reasons
            whyUsSection.Reasons.Remove(reason);
            _context.Update(whyUsSection);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); // Przekierowanie po usunięciu
        }
    }
}
