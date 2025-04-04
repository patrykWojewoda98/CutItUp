using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Website.Models.Abstractions;

namespace Website.Models
{
    public class CartTool
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CartId { get; set; }
        [Required]
        public int ToolId { get; set; }
        [ForeignKey("CartId")]
        public Cart Cart { get; set; } = null!;
        [ForeignKey("ToolId")]
        public Tool Tool { get; set; } = null!;
    }
}
