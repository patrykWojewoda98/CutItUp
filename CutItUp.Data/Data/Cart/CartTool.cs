using CutItUp.Data.Data.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace CutItUp.Data.Data.Cart
{
    public class CartTool
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CartId { get; set; }
        [Required]
        public int ToolId { get; set; }
        [Required]
        public int QuantityOfTools { get; set; } = 1;
        [ForeignKey("CartId")]
        public Cart Cart { get; set; } = null!;
        [ForeignKey("ToolId")]
        public Tool Tool { get; set; } = null!;
    }
}
