using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CutItUp.Data.Data.Cart
{
    public class Cart
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public ICollection<CartTool> Tools { get; set; } = new List<CartTool>();
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public CutItUp.Data.Data.Client.Client Client { get; set; } = null!; 
    }
}
