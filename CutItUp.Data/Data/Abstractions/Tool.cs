using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CutItUp.Data.Data.Abstractions
{
    public abstract class Tool
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Opis")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Cena")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Wymiar")]
        public float Dimension { get; set; }
        [Required]
        [Display(Name = "Materiał")]
        public string Material { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Długość narzędzia")]
        public float Length { get; set; }
        [Required]
        [Display(Name = "Ilość sztuk w magazynie")]
        public int NoOfToolsInMagazine { get; set; }
        [Display(Name = "Zdjęcie")]
        public string? ImageUrl { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Ilość sprzedanych sztuk")]
        public int NoOfSaled { get; set; } = 0;
    }
}
