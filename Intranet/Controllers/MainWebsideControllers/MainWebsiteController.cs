using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CutItUp.Data.Context;
using CutItUp.Data.Data.CMS.MainWebsite;

namespace Intranet.Controllers.MainWebsideControllers
{
    public class MainWebsiteController : Controller
    {
        private readonly CutItUpContext _context;

        public MainWebsiteController(CutItUpContext context)
        {
            _context = context;
        }

        // GET: MainWebsite
        public async Task<IActionResult> Index()
        {
            ViewBag.Promos = await _context.Promo.ToListAsync();
            ViewBag.WhyUsSection = await _context.WhyUsSection.ToListAsync();
            return View(await _context.Promo.ToListAsync());
        }

    }
}
