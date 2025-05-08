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
    public class SpecialToolController : Controller
    {
        private readonly CutItUpContext _context;

        public SpecialToolController(CutItUpContext context)
        {
            _context = context;
        }

        // GET: SpecialTool
        public async Task<IActionResult> Index()
        {
            return View(await _context.SpecialTool.ToListAsync());
        }

        // GET: SpecialTool/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialTools = await _context.SpecialTool
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialTools == null)
            {
                return NotFound();
            }

            return View(specialTools);
        }

    }
}
