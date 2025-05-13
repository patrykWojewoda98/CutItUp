using System.ComponentModel.DataAnnotations;

namespace Intranet.Models.DTO
{
    public class PromoDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string? PromoFileURL { get; set; }
    }
}
