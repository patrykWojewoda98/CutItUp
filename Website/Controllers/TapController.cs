using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CutItUp.Data.Context;
using CutItUp.Data.Data.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace Website.Controllers
{
    public class TapController : Controller
    {
        private readonly CutItUpContext _context;

        public TapController(CutItUpContext context)
        {
            _context = context;
        }

        // GET: Tap
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tap.ToListAsync());
        }

        // GET: Tap/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tap = await _context.Tap
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tap == null)
            {
                return NotFound();
            }

            return View(tap);
        }

       
    }
}
