using System.ComponentModel.DataAnnotations;
using Website.Models.Abstractions;

namespace Website.Models.CMS
{
    public class Drill : Tool
    {
        [Required]
        public float Angle { get; set; }
    }
}
