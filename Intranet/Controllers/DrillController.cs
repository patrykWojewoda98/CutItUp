using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CutItUp.Data.Context;
using CutItUp.Data.Data.Tools;

namespace Intranet.Controllers
{
    public class DrillController : Controller
    {
        private readonly CutItUpContext _context;
        private readonly IWebHostEnvironment _environment;

        public DrillController(CutItUpContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Drill.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var drill = await _context.Drill.FirstOrDefaultAsync(m => m.Id == id);
            if (drill == null) return NotFound();

            return View(drill);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Drill drill, IFormFile ImageFile)
        {
            if (!ModelState.IsValid)
                return View(drill);

            if (ImageFile != null)
            {
                string fileName = $"{Path.GetFileNameWithoutExtension(ImageFile.FileName)}_{DateTime.Now:yyyyMMdd_HHmmss}{Path.GetExtension(ImageFile.FileName)}";
                string uploadPath = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", "Images");
                Directory.CreateDirectory(uploadPath);
                string fullPath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                drill.ImageUrl = $"/Images/{fileName}";
            }

            _context.Add(drill);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Tool");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var drill = await _context.Drill.FindAsync(id);
            if (drill == null) return NotFound();

            return View(drill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Drill updatedDrill, IFormFile ImageFile)
        {
            if (id != updatedDrill.Id) return NotFound();

            var existingDrill = await _context.Drill.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
            if (existingDrill == null) return NotFound();

            ModelState.Remove("ImageFile");
            if (!ModelState.IsValid) return View(updatedDrill);

            if (ImageFile != null)
            {
                if (!string.IsNullOrEmpty(existingDrill.ImageUrl))
                {
                    string oldImagePath = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", existingDrill.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                        System.IO.File.Delete(oldImagePath);
                }

                string fileName = $"{Path.GetFileNameWithoutExtension(ImageFile.FileName)}_{DateTime.Now:yyyyMMdd_HHmmss}{Path.GetExtension(ImageFile.FileName)}";
                string uploadPath = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", "Images");
                Directory.CreateDirectory(uploadPath);
                string fullPath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                updatedDrill.ImageUrl = $"/Images/{fileName}";
            }
            else
            {
                updatedDrill.ImageUrl = existingDrill.ImageUrl;
            }

            _context.Update(updatedDrill);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Tool");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var drill = await _context.Drill.FirstOrDefaultAsync(m => m.Id == id);
            if (drill == null) return NotFound();

            return View(drill);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drill = await _context.Drill.FindAsync(id);
            if (drill != null)
            {
                if (!string.IsNullOrEmpty(drill.ImageUrl))
                {
                    string fullImagePath = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", drill.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(fullImagePath))
                        System.IO.File.Delete(fullImagePath);
                }

                _context.Drill.Remove(drill);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Tool");
        }
    }
}
