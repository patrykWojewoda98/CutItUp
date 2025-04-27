using CutItUp.Data.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace CutItUp.Data.Data.Tools
{
    public class SpecialTool : Tool
    {
        [Required]
        [Display(Name = "Libcza ostrzy")]
        public int NoCuttingEddges { get; set; }
        [Display(Name = "Kąt")]
        public float? Angle { get; set; }
        [Display(Name = "Promień")]
        public float? Radius { get; set; }
    }
}
