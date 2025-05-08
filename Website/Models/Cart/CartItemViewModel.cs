namespace Website.Models.Cart
{
    public class CartItemViewModel
    {
        public int CartToolId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }

        public decimal Total => Price * Quantity;
    }
}
