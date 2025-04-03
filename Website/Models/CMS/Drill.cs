using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Website.Models.CMS
{
    public class Drill
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public float Angle { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        [Required]
        public float Length { get; set; }
        [Required]
        public float Dimension { get; set; }
        [Required]
        public string Material { get; set; } = string.Empty;
    }
}
