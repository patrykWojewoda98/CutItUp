using CutItUp.Data.Data.Cart;
using Website.Models.Cart;

namespace Website.Services.Interfaces
{
    public interface ICartService
    {
        Task AddToCartAsync(int toolId, int quantity, int ClientId);
        Task<List<CartItemViewModel>> GetCartItemsAsync(int clientId);
        Task RemoveFromCartAsync(int cartToolId);
        Task UpdateQuantityAsync(int cartToolId, int quantity);
    }
}
