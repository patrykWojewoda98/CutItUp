using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CutItUp.Data.Context;
using CutItUp.Data.Data.Tools;

namespace Intranet.Controllers
{
    public class TapController : Controller
    {
        private readonly CutItUpContext _context;
        private readonly IWebHostEnvironment _environment;

        public TapController(CutItUpContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Tap.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var tap = await _context.Tap.FirstOrDefaultAsync(m => m.Id == id);
            if (tap == null) return NotFound();

            return View(tap);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tap tap, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    var safeFileName = $"{Path.GetFileNameWithoutExtension(ImageFile.FileName)}_{DateTime.Now:yyyyMMdd_HHmmss}{Path.GetExtension(ImageFile.FileName)}";
                    string uploadPath = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", "Images");
                    Directory.CreateDirectory(uploadPath);
                    string fullPath = Path.Combine(uploadPath, safeFileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    tap.ImageUrl = $"/Images/{safeFileName}";
                }
                else
                {
                    tap.ImageUrl = "/Images/default.png"; 
                }

                _context.Add(tap);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Tool");
            }

            return View(tap);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tap = await _context.Tap.FindAsync(id);
            if (tap == null) return NotFound();

            return View(tap);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tap tap, IFormFile ImageFile)
        {
            if (id != tap.Id) return NotFound();


            ModelState.Remove("ImageFile");
            if (ModelState.IsValid)
            {
                try
                {
                    var existingTap = await _context.Tap.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

                    if (ImageFile != null)
                    {
                        // Usuń stare zdjęcie
                        if (!string.IsNullOrEmpty(existingTap.ImageUrl))
                        {
                            var oldImagePath = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", existingTap.ImageUrl.TrimStart('/'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        // Zapisz nowe zdjęcie
                        var safeFileName = $"{Path.GetFileNameWithoutExtension(ImageFile.FileName)}_{DateTime.Now:yyyyMMdd_HHmmss}{Path.GetExtension(ImageFile.FileName)}";
                        string uploadPath = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", "Images");
                        Directory.CreateDirectory(uploadPath);
                        string fullPath = Path.Combine(uploadPath, safeFileName);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(stream);
                        }

                        tap.ImageUrl = $"/Images/{safeFileName}";
                    }
                    else
                    {
                        tap.ImageUrl = existingTap.ImageUrl;
                    }

                    _context.Update(tap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TapExists(tap.Id)) return NotFound();
                    throw;
                }

                return RedirectToAction("Index", "Tool");
            }

            return View(tap);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var tap = await _context.Tap.FirstOrDefaultAsync(m => m.Id == id);
            if (tap == null) return NotFound();

            return View(tap);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tap = await _context.Tap.FindAsync(id);
            if (tap != null)
            {
                if (!string.IsNullOrEmpty(tap.ImageUrl))
                {
                    var imagePath = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", tap.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                _context.Tap.Remove(tap);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Tool");
        }

        private bool TapExists(int id)
        {
            return _context.Tap.Any(e => e.Id == id);
        }
    }
}
