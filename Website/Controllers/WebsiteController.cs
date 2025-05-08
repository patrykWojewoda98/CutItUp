using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CutItUp.Data.Context;

namespace Website.Controllers
{
    public class WebsiteController : Controller
    {
        private readonly CutItUpContext _context;

        public WebsiteController(CutItUpContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Show(int id)
        {
            if (id == null)
                return NotFound();

            var website = await _context.Website
                .Include(w => w.TitleDecriptionParts)
                .Include(w => w.ListParts)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (website == null)
                return NotFound();

            return View("Details", website);
        }
    }
}
