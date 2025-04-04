using System.ComponentModel.DataAnnotations;
using Website.Models.Abstractions;

namespace Website.Models.CMS
{
    public class SpecialTools : Tool
    {
        [Required]
        public int NoCuttingEddges { get; set; }

        public float? Angle { get; set; }
        public float? Radius { get; set; }
    }
}
