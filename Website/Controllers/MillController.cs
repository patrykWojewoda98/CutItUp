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
    public class MillController : Controller
    {
        private readonly CutItUpContext _context;

        public MillController(CutItUpContext context)
        {
            _context = context;
        }

        // GET: Mill
        public async Task<IActionResult> Index()
        {
            return View(await _context.Mill.ToListAsync());
        }

        // GET: Mill/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mill = await _context.Mill
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mill == null)
            {
                return NotFound();
            }

            return View(mill);
        }
    }
}
