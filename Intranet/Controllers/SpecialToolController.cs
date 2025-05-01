using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CutItUp.Data.Context;
using CutItUp.Data.Data.Tools;
using Microsoft.AspNetCore.Hosting;

namespace Intranet.Controllers
{
    public class SpecialToolController : Controller
    {
        private readonly CutItUpContext _context;
        private readonly IWebHostEnvironment _environment;

        public SpecialToolController(CutItUpContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.SpecialTool.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var specialTool = await _context.SpecialTool.FirstOrDefaultAsync(m => m.Id == id);
            if (specialTool == null) return NotFound();

            return View(specialTool);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTool specialTool, IFormFile? ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    var fileName = $"{Path.GetFileNameWithoutExtension(ImageFile.FileName)}_{DateTime.Now:yyyyMMdd_HHmmss}{Path.GetExtension(ImageFile.FileName)}";
                    string uploadPath = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", "Images");
                    Directory.CreateDirectory(uploadPath);
                    string fullPath = Path.Combine(uploadPath, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    specialTool.ImageUrl = $"/Images/{fileName}";
                }

                _context.Add(specialTool);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Tool");
            }
            return View(specialTool);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var specialTool = await _context.SpecialTool.FindAsync(id);
            if (specialTool == null) return NotFound();

            return View(specialTool);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SpecialTool updatedTool, IFormFile? ImageFile)
        {
            if (id != updatedTool.Id) return NotFound();

            var existingTool = await _context.SpecialTool.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (existingTool == null) return NotFound();

            if (!ModelState.IsValid) return View(updatedTool);

            if (ImageFile != null)
            {
                // Usuń stare zdjęcie jeśli istnieje
                if (!string.IsNullOrEmpty(existingTool.ImageUrl))
                {
                    string oldPath = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", existingTool.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                var fileName = $"{Path.GetFileNameWithoutExtension(ImageFile.FileName)}_{DateTime.Now:yyyyMMdd_HHmmss}{Path.GetExtension(ImageFile.FileName)}";
                string uploadPath = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", "Images");
                Directory.CreateDirectory(uploadPath);
                string fullPath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                updatedTool.ImageUrl = $"/Images/{fileName}";
            }
            else
            {
                updatedTool.ImageUrl = existingTool.ImageUrl;
            }

            _context.Update(updatedTool);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Tool");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var specialTool = await _context.SpecialTool.FirstOrDefaultAsync(m => m.Id == id);
            if (specialTool == null) return NotFound();

            return View(specialTool);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialTool = await _context.SpecialTool.FindAsync(id);
            if (specialTool != null)
            {
                if (!string.IsNullOrEmpty(specialTool.ImageUrl))
                {
                    string fullPath = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", specialTool.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }

                _context.SpecialTool.Remove(specialTool);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Tool");
        }

    }
}
