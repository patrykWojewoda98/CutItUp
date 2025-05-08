using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CutItUp.Data.Data
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Display(Name = "Status zamówienia")]
        public string Status { get; set; } = "Oczekujące";

        [Required]
        public int CartToolId { get; set; }

        [ForeignKey("CartId")]
        public CutItUp.Data.Data.Cart.CartTool CartTool { get; set; } = null!;
        
        [Required]
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public CutItUp.Data.Data.Client.Client Client { get; set; } = null!;

    }
}
