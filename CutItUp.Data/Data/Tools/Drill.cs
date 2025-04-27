using CutItUp.Data.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace CutItUp.Data.Data.Tools
{
    public class Drill : Tool
    {
        [Required]
        [Display(Name = "Kąt")]
        public float Angle { get; set; }
    }
}
