using System.ComponentModel.DataAnnotations;
using Website.Models.Abstractions;

namespace Website.Models
{
    public class Cart
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public ICollection<CartTool> Tools { get; set; } = new List<CartTool>();

    }
}
