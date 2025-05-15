using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CutItUp.Data.Context;
using CutItUp.Data.Data.Client;
using System.Text;
using System.Security.Cryptography;
using Website.Models.ClientRegister;
using Website.Services;

namespace Website.Controllers
{
    public class ClientController : Controller
    {
        private readonly CutItUpContext _context;
        private readonly JwtService _jwtService;

        public ClientController(CutItUpContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        // GET: Client
        public async Task<IActionResult> Index()
        {
            return View(await _context.Client.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, bool RememberMe)
        {
            var hashedPassword = HashPassword(password);

            var client = await _context.Client.FirstOrDefaultAsync(u => u.Login == username && u.PasswordHash == hashedPassword);
            if (client != null)
            {
                //HttpContext.Session.SetString("ClientToken", _jwtService.GenerateToken(client)); tego już nie potrzebujemy gdyż działamy na plikakch cookie

                if (RememberMe)
                {
                    Response.Cookies.Append("ClientToken", _jwtService.GenerateToken(client), new CookieOptions
                    {
                        MaxAge = TimeSpan.FromDays(30),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None
                    });
                }
                else
                {
                    // cookie sesyjne (zniknie po zamknięciu przeglądarki)
                    Response.Cookies.Append("ClientToken", _jwtService.GenerateToken(client), new CookieOptions
                    {
                        Expires = null,
                        MaxAge = TimeSpan.FromMinutes(30),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None
                    });
                }
                return RedirectToAction("Index", "CartTool");
            }
            

            ViewBag.LoginFailed = true;
            return View("Index");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Usuń ciasteczko z tokenem
            if (Request.Cookies.ContainsKey("ClientToken"))
            {
                Response.Cookies.Delete("ClientToken");
            }

            // Wyczyść sesję (jeśli coś jeszcze w niej trzymasz)
            HttpContext.Session.Clear();

            // Przekieruj na stronę logowania
            return RedirectToAction("Index", "CartTool");
        }

        // GET: Client/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Client/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = new Client
                {
                    Login = model.Login,
                    PasswordHash = HashPassword(model.Password),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    City = model.City,
                    PostalCode = model.PostalCode,
                    Country = model.Country
                };

                _context.Client.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }


        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PasswordHash,Login,FirstName,LastName,Email,PhoneNumber,Address,City,PostalCode,Country")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Client.FindAsync(id);
            if (client != null)
            {
                _context.Client.Remove(client);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.Id == id);
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
