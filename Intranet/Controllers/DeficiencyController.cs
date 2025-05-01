using CutItUp.Data.Context;
using CutItUp.Data.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Intranet.Controllers
{
    public class DeficiencyController : Controller
    {
        private readonly CutItUpContext _context;
        private readonly IWebHostEnvironment _environment;

        public DeficiencyController(CutItUpContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        




        public async Task<IActionResult> Index(string toolType)
        {
            ViewBag.ListOfDeficiencies = new List<Tool>();

            foreach (var tool in _context.Tool.ToList())
            {
                if (tool.NoOfToolsInMagazine < 10)
                {
                    ViewBag.ListOfDeficiencies.Add(tool);
                }
            }

            return View();
        }
    }
}
