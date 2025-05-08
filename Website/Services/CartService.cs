using CutItUp.Data.Context;
using CutItUp.Data.Data.Cart;
using Microsoft.EntityFrameworkCore;
using Website.Models.Cart;
using Website.Services.Interfaces;

public class CartService : ICartService
{
    private readonly CutItUpContext _context;

    public CartService(CutItUpContext context)
    {
        _context = context;
    }

    public async Task AddToCartAsync(int toolId, int quantity, int clientId)
    {
        var cart = await GetOrCreateCartAsync(clientId);

        var existingItem = await _context.CartTool
            .FirstOrDefaultAsync(ct => ct.CartId == cart.Id && ct.ToolId == toolId);

        if (existingItem != null)
        {
            existingItem.QuantityOfTools += quantity;
        }
        else
        {
            _context.CartTool.Add(new CartTool
            {
                CartId = cart.Id,
                ToolId = toolId,
                QuantityOfTools = quantity
            });
        }

        await _context.SaveChangesAsync();
    }
    public async Task RemoveFromCartAsync(int cartToolId)
    {
        var item = await _context.CartTool.FindAsync(cartToolId);
        if (item != null)
        {
            _context.CartTool.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateQuantityAsync(int cartToolId, int quantity)
    {
        var item = await _context.CartTool.FindAsync(cartToolId);
        if (item != null && quantity > 0)
        {
            item.QuantityOfTools = quantity;
            await _context.SaveChangesAsync();
        }
    }

    

    private async Task<Cart> GetOrCreateCartAsync(int clientId)
    {
        var cart = await _context.Cart.FirstOrDefaultAsync(c => c.ClientId == clientId);
        if (cart == null)
        {
            cart = new Cart();
            cart.ClientId = clientId;
            _context.Cart.Add(cart);
            await _context.SaveChangesAsync();
        }
        return cart;
    }

    public async Task<List<CartItemViewModel>> GetCartItemsAsync(int clientId)
    {
        var cart = await GetOrCreateCartAsync(clientId);

        return await _context.CartTool
            .Include(ct => ct.Tool)
            .Where(ct => ct.CartId == cart.Id)
            .Select(ct => new CartItemViewModel
            {
                CartToolId = ct.Id,
                Name = ct.Tool.Name,
                Description = ct.Tool.Description,
                Price = ct.Tool.Price,
                Quantity = ct.QuantityOfTools,
                ImageUrl = ct.Tool.ImageUrl
            })
            .ToListAsync();
    }
}
