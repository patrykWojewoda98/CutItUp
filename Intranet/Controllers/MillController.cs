using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CutItUp.Data.Context;
using CutItUp.Data.Data.Tools;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Controllers
{
    public class MillController : Controller
    {
        private readonly CutItUpContext _context;
        private readonly IWebHostEnvironment _environment;

        public MillController(CutItUpContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Mill
        public async Task<IActionResult> Index()
        {
            return View(await _context.Mill.ToListAsync());
        }

        // GET: Mill/Create
        public IActionResult Create() => View();

        // POST: Mill/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Mill mill, IFormFile? ImageFile)
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

                    mill.ImageUrl = $"/Images/{safeFileName}";
                }
                else
                {
                    mill.ImageUrl = "/Images/default.png"; 
                }

                _context.Add(mill);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Tool");
            }
            return View(mill);
        }

        // GET: Mill/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var mill = await _context.Mill.FindAsync(id);
            if (mill == null) return NotFound();

            return View(mill);
        }

        // POST: Mill/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Mill mill, IFormFile? ImageFile)
        {
            if (id != mill.Id) return NotFound();


            ModelState.Remove("ImageFile");
            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageFile != null)
                    {
                        // Usuń stare zdjęcie jeśli istnieje
                        if (!string.IsNullOrEmpty(mill.ImageUrl))
                        {
                            var oldPath = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", mill.ImageUrl.TrimStart('/'));
                            if (System.IO.File.Exists(oldPath))
                                System.IO.File.Delete(oldPath);
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

                        mill.ImageUrl = $"/Images/{safeFileName}";
                    }

                    _context.Update(mill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MillExists(mill.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction("Index", "Tool");
            }

            return View(mill);
        }

        // GET: Mill/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var mill = await _context.Mill.FirstOrDefaultAsync(m => m.Id == id);
            if (mill == null) return NotFound();

            return View(mill);
        }

        // POST: Mill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mill = await _context.Mill.FindAsync(id);
            if (mill != null)
            {
                // Usuń zdjęcie z dysku
                if (!string.IsNullOrEmpty(mill.ImageUrl))
                {
                    var filePath = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", mill.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);
                }

                _context.Mill.Remove(mill);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Tool");
        }

        private bool MillExists(int id) => _context.Mill.Any(e => e.Id == id);
    }
}
