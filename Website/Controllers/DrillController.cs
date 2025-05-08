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
    public class DrillController : Controller
    {
        private readonly CutItUpContext _context;

        public DrillController(CutItUpContext context)
        {
            _context = context;
        }

        // GET: Drill
        public async Task<IActionResult> Index()
        {
            return View(await _context.Drill.ToListAsync());
        }

        // GET: Drill/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drill = await _context.Drill
                .FirstOrDefaultAsync(m => m.Id == id);
            if (drill == null)
            {
                return NotFound();
            }

            return View(drill);
        }
    }
}
