using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CutItUp.Data.Context;
using CutItUp.Data.Data.User;
using System.Text;
using System.Security.Cryptography;
using Intranet.Models;
using System.Diagnostics;

namespace Intranet.Controllers
{
    public class UserController : Controller
    {
        private readonly CutItUpContext _context;
        private readonly IWebHostEnvironment _environment;

        public UserController(CutItUpContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            Debug.WriteLine("Uruchomiłem sie" );
            Debug.WriteLine("UserId: " + HttpContext.Session.GetInt32("UserId"));
            return View(await _context.User.Where(u=>u.Position!="Client").ToListAsync());
        }

        

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User
            {
                Name = model.Name,
                Surname = model.Surname,
                Login = model.Login,
                Position = model.Position,
                Salary = model.Salary,
                PasswordHash = string.IsNullOrEmpty(model.Password) ? string.Empty : HashPassword(model.Password)
            };

            if (model.ImageFile != null)
            {
                var fileName = $"{Path.GetFileNameWithoutExtension(model.ImageFile.FileName)}_{DateTime.Now:yyyyMMdd_HHmmss}{Path.GetExtension(model.ImageFile.FileName)}";
                var uploadPath = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", "Images");
                Directory.CreateDirectory(uploadPath);
                var fullPath = Path.Combine(uploadPath, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await model.ImageFile.CopyToAsync(stream);

                user.ImageUrl = $"/Images/{fileName}";
            }

            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return NotFound();

            var model = new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Login = user.Login,
                Position = user.Position,
                Salary = user.Salary,
                ExistingImageUrl = user.ImageUrl
            };

            return View(model); 
        }

        public async Task<IActionResult> EditAccount(int? userId)
        {
            if (userId == null) return NotFound();

            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) return NotFound();

            var model = new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Login = user.Login,
                Position = user.Position,
                Salary = user.Salary,
                ExistingImageUrl = user.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (user == null) return NotFound();

            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Login = model.Login;
            user.Position = model.Position;
            user.Salary = model.Salary;

            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = HashPassword(model.Password);

            if (model.ImageFile != null)
            {
                // Usuń stare zdjęcie
                if (!string.IsNullOrEmpty(user.ImageUrl))
                {
                    var oldPath = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", user.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                }

                var fileName = $"{Path.GetFileNameWithoutExtension(model.ImageFile.FileName)}_{DateTime.Now:yyyyMMdd_HHmmss}{Path.GetExtension(model.ImageFile.FileName)}";
                var uploadPath = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", "Images");
                Directory.CreateDirectory(uploadPath);
                var fullPath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                user.ImageUrl = $"/Images/{fileName}";
            }

            _context.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccount(UserModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _context.User.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (user == null) return NotFound();

            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Login = model.Login;
            user.Position = model.Position;
            user.Salary = model.Salary;

            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = HashPassword(model.Password);

            if (model.ImageFile != null)
            {
                // Usuń stare zdjęcie
                if (!string.IsNullOrEmpty(user.ImageUrl))
                {
                    var oldPath = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", user.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                }

                var fileName = $"{Path.GetFileNameWithoutExtension(model.ImageFile.FileName)}_{DateTime.Now:yyyyMMdd_HHmmss}{Path.GetExtension(model.ImageFile.FileName)}";
                var uploadPath = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", "Images");
                Directory.CreateDirectory(uploadPath);
                var fullPath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                user.ImageUrl = $"/Images/{fileName}";
            }

            _context.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.User.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(user.ImageUrl))
                {
                    var path = Path.Combine(_environment.ContentRootPath, "..", "CutItUp.Data", "Data", user.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                }

                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var hashedPassword = HashPassword(password);

            var user = await _context.User.FirstOrDefaultAsync(u => u.Login == username && u.PasswordHash == hashedPassword);
            if (user != null)
            {
                ViewBag.LoginFailed = false;
                HttpContext.Session.SetString("UserName", user.Name + " "+ user.Surname);
                HttpContext.Session.SetString("ProfileImagePath", user.ImageUrl);
                HttpContext.Session.SetInt32("UserId", user.Id);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.LoginFailed = true;
            return View("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
                HttpContext.Session.Remove("UserName");
                HttpContext.Session.Remove("ProfileImagePath");
                HttpContext.Session.Remove("UserID");
             
            return View("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? userId)
        {
            if (userId == null) return NotFound();

            var user = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return NotFound();

            var model = new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Login = user.Login,
                Position = user.Position,
                Salary = user.Salary,
                ExistingImageUrl = user.ImageUrl
            };

            return View("Details", model);
        }
    }
}
