using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CutItUp.Data.Context;
using CutItUp.Data.Data.CMS.MainWebsite;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using Intranet.Models.DTO;

namespace Intranet.Controllers.MainWebsideControllers
{
    public class PromoController : Controller
    {
        private readonly CutItUpContext _context;
        private readonly IWebHostEnvironment _environment;

        public PromoController(CutItUpContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Promo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Promo.ToListAsync());
        }

        // GET: Promo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promo = await _context.Promo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (promo == null)
            {
                return NotFound();
            }

            return View(promo);
        }

        // GET: Promo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Promo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PromoDTO promo, IFormFile PromoFile)
        {
            if (ModelState.IsValid && PromoFile != null)
            {
                var safeFileName = $"{Path.GetFileNameWithoutExtension(PromoFile.FileName)}_{DateTime.Now:yyyyMMdd_HHmmss}{Path.GetExtension(PromoFile.FileName)}";
                var videoDir = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", "Video");
                Directory.CreateDirectory(videoDir);

                var path = Path.Combine(videoDir, safeFileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await PromoFile.CopyToAsync(stream);
                }
                var finalPromo = new Promo
                {
                    Title = promo.Title,
                    Description = promo.Description,
                    PromoFileURL = $"/Video/{safeFileName}"
                };

                promo.PromoFileURL = $"/Video/{safeFileName}";
                _context.Add(finalPromo);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "MainWebsite");
            }

            ModelState.AddModelError("PromoFile", "Plik promocyjny jest wymagany.");
            return View(promo);
        }





        // GET: Promo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promo = await _context.Promo.FindAsync(id);
            if (promo == null)
            {
                return NotFound();
            }
            return View(promo);
        }

        // POST: Promo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,PromoFileURL")] Promo promo, IFormFile PromoFile)
        {
            if (id != promo.Id)
            {
                return NotFound();
            }

            var existingPromo = await _context.Promo.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (existingPromo == null)
            {
                return NotFound();
            }

            string promoPath = existingPromo.PromoFileURL;

            if (PromoFile != null)
            {
                // Ścieżka zapisu
                string uploadsFolder = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", "Promos");
                Directory.CreateDirectory(uploadsFolder);

                // Nowa nazwa pliku
                var fileName = $"{Path.GetFileNameWithoutExtension(PromoFile.FileName)}_{DateTime.Now:yyyyMMdd_HHmmss}{Path.GetExtension(PromoFile.FileName)}";
                var fullPath = Path.Combine(uploadsFolder, fileName);

                // Zapis pliku
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await PromoFile.CopyToAsync(stream);
                }

                // Usuń stary plik, jeśli istnieje
                if (!string.IsNullOrEmpty(promoPath))
                {
                    var oldPath = Path.Combine(uploadsFolder, Path.GetFileName(promoPath));
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                // Nowy path
                promo.PromoFileURL = $"/Promos/{fileName}";
            }
            else
            {
                // Nie zmieniaj ścieżki, jeśli nie przesłano nowego pliku
                promo.PromoFileURL = promoPath;
            }

            ModelState.Remove("PromoFile");
            ModelState.Remove("PromoFileURL");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Promo.Any(e => e.Id == promo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "MainWebsite");
            }

            return View(promo);
        }


        // GET: Promo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promo = await _context.Promo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (promo == null)
            {
                return NotFound();
            }

            return View(promo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var promo = await _context.Promo.FindAsync(id);
            if (promo != null)
            {
                if (!string.IsNullOrEmpty(promo.PromoFileURL))
                {
                    string uploadsFolder = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", "Promos");
                    string filePath = Path.Combine(uploadsFolder, Path.GetFileName(promo.PromoFileURL));

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _context.Promo.Remove(promo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
