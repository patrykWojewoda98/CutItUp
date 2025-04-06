using System.Diagnostics;
using CutItUp.Data.Context;
using CutItUp.Data.Data.Abstractions;
using CutItUp.Data.Data.Tools;
using Microsoft.AspNetCore.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CutItUpContext _context;

        public HomeController(ILogger<HomeController> logger, CutItUpContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Promos = (
                from promo in _context.Promo 
                select promo).ToList();

            int noOfBestsellers = 3;
            var bestsellers = new List<Tool>();
            bestsellers.AddRange(_context.Mill.OrderBy(m => m.NoOfSaled).Take(noOfBestsellers));
            bestsellers.AddRange(_context.Drill.OrderBy(m => m.NoOfSaled).Take(noOfBestsellers));
            bestsellers.AddRange(_context.Tap.OrderBy(m => m.NoOfSaled).Take(noOfBestsellers));
            bestsellers.AddRange(_context.SpecialTool.OrderBy(m => m.NoOfSaled).Take(noOfBestsellers));
            ViewBag.Bestsellers = bestsellers.OrderBy(t=>t.NoOfSaled).Take(noOfBestsellers);

            ViewBag.WhyUsSection = _context.WhyUsSection.ToList();

            return View();
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
