using CutItUp.Data.Context;
using CutItUp.Data.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Intranet.Controllers
{
    public class ToolController : Controller
    {
        private readonly CutItUpContext _context;
        private readonly IWebHostEnvironment _environment;

        public ToolController(CutItUpContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        private List<string> GetToolTypes()
        {
            var baseType = typeof(Tool);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly =>
                {
                    try
                    {
                        return assembly.GetTypes();
                    }
                    catch
                    {
                        return Array.Empty<Type>();
                    }
                })
                .Where(t => t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t))
                .Select(t => t.Name)
                .ToList();

            return types;
        }


        public IActionResult Create(string toolType = null)
        {
            var types = new List<SelectListItem>
    {
        new SelectListItem { Value = "Drill", Text = "Wiertło" },
        new SelectListItem { Value = "Mill", Text = "Frez" },
        new SelectListItem { Value = "Tap", Text = "Gwintownik" },
        new SelectListItem { Value = "SpecialTool", Text = "Narzędzie specjalne" }
    };

            ViewBag.ToolTypes = types;
            ViewBag.SelectedType = toolType ?? types.FirstOrDefault()?.Value;

            return View();
        }



        public async Task<IActionResult> Index(string toolType)
        {
            ViewBag.ToolTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "Drill", Text = "Wiertła" },
                new SelectListItem { Value = "Mill", Text = "Frezy" },
                new SelectListItem { Value = "Tap", Text = "Gwintowniki" },
                new SelectListItem { Value = "SpecialTool", Text = "Narzędzia specjalne" }
        };
            ViewBag.SelectedType = toolType;

            switch (toolType?.ToLower())
            {
                case "drill":
                    ViewBag.Tools = await _context.Drill.ToListAsync();
                    break;
                case "mill":
                    ViewBag.Tools = await _context.Mill.ToListAsync();
                    break;
                case "tap":
                    ViewBag.Tools = await _context.Tap.ToListAsync();
                    break;
                case "specialtool":
                    ViewBag.Tools = await _context.SpecialTool.ToListAsync();
                    break;
                default:
                    ViewBag.Tools = new List<Tool>();
                    break;
            }

            return View();
        }

        //public IActionResult Create()
        //{
        //    ViewBag.ToolTypes = new List<string> { "Drill", "Mill", "Tap", "SpecialTool" };
        //    return View();
        //}

        //public async Task<IActionResult> Index()
        //{
        //    ViewBag.Drills = await _context.Drill.ToListAsync();
        //    ViewBag.Mills = await _context.Mill.ToListAsync();
        //    ViewBag.Taps = await _context.Tap.ToListAsync();
        //    ViewBag.SpecialTools = await _context.SpecialTool.ToListAsync();


        //    return View(await _context.Drill.ToListAsync());
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(ToolViewModel model)
        //{
        //    Debug.WriteLine($"ToolType: {model.ToString()}");

        //    if (!ModelState.IsValid)
        //    {
        //        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        //        {
        //            Debug.WriteLine("Błąd walidacji: " + error.ErrorMessage);
        //        }
        //        return View(model);
        //    }

        //    string imagePath = string.Empty;
        //    if (model.ImageFile != null)
        //    {
        //        var safeFileName = $"{Path.GetFileNameWithoutExtension(model.ImageFile.FileName)}_{DateTime.Now:yyyyMMdd_HHmmss}{Path.GetExtension(model.ImageFile.FileName)}";
        //        string uploadPath = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", "Images");
        //        Directory.CreateDirectory(uploadPath);
        //        string fullPath = Path.Combine(uploadPath, safeFileName);

        //        using (var stream = new FileStream(fullPath, FileMode.Create))
        //        {
        //            await model.ImageFile.CopyToAsync(stream);
        //        }

        //        imagePath = $"/Images/{safeFileName}";
        //    }

        //    Tool tool = model.ToolType switch
        //    {
        //        ToolType.Drill => new Drill { Angle = model.Angle ?? 118f },
        //        ToolType.Mill => new Mill { NoCuttingEddges = model.NoCuttingEddges ?? 4 },
        //        ToolType.Tap => new Tap { PassThrough = model.PassThrough ?? false },
        //        ToolType.SpecialTool => new SpecialTool
        //        {
        //            NoCuttingEddges = model.NoCuttingEddges ?? 1,
        //            Angle = model.Angle,
        //            Radius = model.Radius
        //        },
        //        _ => throw new ArgumentException("Nieznany typ narzędzia")
        //    };

        //    tool.Name = model.Name;
        //    tool.Description = model.Description;
        //    tool.Price = model.Price;
        //    tool.Dimension = model.Dimension;
        //    tool.Material = model.Material;
        //    tool.Length = model.Length;
        //    tool.NoOfToolsInMagazine = model.NoOfToolsInMagazine;
        //    tool.NoOfSaled = model.NoOfSaled;
        //    tool.ImageUrl = imagePath;

        //    _context.Add(tool);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction("Index", "Website");
        //}
    }


}