using System.ComponentModel.DataAnnotations;

namespace Website.Models.ToolModels
{
    public class ToolViewModel
    {
        [Required]
        public ToolType ToolType { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public float Dimension { get; set; }
        [Required]
        public string Material { get; set; }
        [Required]
        public float Length { get; set; }
        [Required]
        public int NoOfToolsInMagazine { get; set; }
        [Required]
        public int NoOfSaled { get; set; }

        
        public float? Angle { get; set; }
        public int? NoCuttingEddges { get; set; }
        public float? Radius { get; set; }
        public bool? PassThrough { get; set; }

        public IFormFile? ImageFile { get; set; }
        
    }
}
