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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditReason(int id, string originalReason, string updatedReason)
        {
            var whyUsSection = await _context.WhyUsSection
                .FirstOrDefaultAsync(w => w.Id == id);

            if (whyUsSection == null)
            {
                return NotFound();
            }

            var reasonsList = whyUsSection.Reasons.ToList();

            int index = reasonsList.IndexOf(originalReason);
            if (index != -1 && !string.IsNullOrWhiteSpace(updatedReason))
            {
                reasonsList[index] = updatedReason;
                whyUsSection.Reasons = reasonsList;
                _context.Update(whyUsSection);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "MainWebsite");
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

            whyUsSection.Reasons.Add(newReason);

            _context.Update(whyUsSection);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "MainWebsite");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReason(string reason, int id)
        {
            var whyUsSection = await _context.WhyUsSection
                .FirstOrDefaultAsync(w => w.Id == id);

            if (whyUsSection == null)
            {
                return NotFound();
            }

            whyUsSection.Reasons.Remove(reason);
            _context.Update(whyUsSection);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "MainWebsite"); 
        }
    }
}
