using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CutItUp.Data.Context;
using CutItUp.Data.Data.Cart;
using Website.Services.Interfaces;
using Website.Models;

namespace Website.Controllers
{
    public class CartToolController : Controller
    {
        private readonly CutItUpContext _context;
        private readonly ICartService _cartService;

        public CartToolController(CutItUpContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int itemId, int amount, int clientID)
        {
            await _cartService.AddToCartAsync(itemId, amount, clientID);
            return RedirectToAction("Index", "CartTool");
        }

        // GET: CartTool
        public async Task<IActionResult> Index()
        {
            var tokenInfo = HttpContext.Items["TokenInfo"] as TokenInfo;
            var clientId = tokenInfo?.Id;
            //var clientId = HttpContext.Session.GetInt32("ClientId");
            if (!clientId.HasValue)
            {
                return RedirectToAction("Index", "Client");
            }

            var cartItems = await _cartService.GetCartItemsAsync(clientId.Value);
            decimal totalAmount = cartItems.Sum(item => item.Price * item.Quantity);

            ViewBag.TotalAmount = totalAmount;
            return View(cartItems); 
        }

        // GET: CartTool/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartTool = await _context.CartTool
                .Include(c => c.Cart)
                .Include(c => c.Tool)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartTool == null)
            {
                return NotFound();
            }

            return View(cartTool);
        }

        // GET: CartTool/Create
        public IActionResult Create()
        {
            ViewData["CartId"] = new SelectList(_context.Cart, "Id", "Id");
            ViewData["ToolId"] = new SelectList(_context.Tool, "Id", "Description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CartId,ToolId,QuantityOfTools")] CartTool cartTool)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartTool);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "Id", "Id", cartTool.CartId);
            ViewData["ToolId"] = new SelectList(_context.Tool, "Id", "Description", cartTool.ToolId);
            return View(cartTool);
        }

        // GET: CartTool/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartTool = await _context.CartTool.FindAsync(id);
            if (cartTool == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "Id", "Id", cartTool.CartId);
            ViewData["ToolId"] = new SelectList(_context.Tool, "Id", "Description", cartTool.ToolId);
            return View(cartTool);
        }

        // POST: CartTool/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CartId,ToolId,QuantityOfTools")] CartTool cartTool)
        {
            if (id != cartTool.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartTool);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartToolExists(cartTool.Id))
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
            ViewData["CartId"] = new SelectList(_context.Cart, "Id", "Id", cartTool.CartId);
            ViewData["ToolId"] = new SelectList(_context.Tool, "Id", "Description", cartTool.ToolId);
            return View(cartTool);
        }

        // GET: CartTool/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartTool = await _context.CartTool
                .Include(c => c.Cart)
                .Include(c => c.Tool)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartTool == null)
            {
                return NotFound();
            }

            return View(cartTool);
        }
        [HttpPost, ActionName("RemoveItem")]
        public async Task<IActionResult> RemoveItem(int? itemId)
        {
            if (itemId == null)
            {
                return NotFound();
            }

            await _cartService.RemoveFromCartAsync(itemId.Value);

            return RedirectToAction("Index", "CartTool");
        }

        [HttpPost, ActionName("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity(int itemId, int quantity)
        {
            await _cartService.UpdateQuantityAsync(itemId, quantity);
            return RedirectToAction("Index");
        }

        // POST: CartTool/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartTool = await _context.CartTool.FindAsync(id);
            if (cartTool != null)
            {
                _context.CartTool.Remove(cartTool);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartToolExists(int id)
        {
            return _context.CartTool.Any(e => e.Id == id);
        }
    }
}
