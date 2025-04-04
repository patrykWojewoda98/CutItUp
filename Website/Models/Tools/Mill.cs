using System.ComponentModel.DataAnnotations;
using Website.Models.Abstractions;

namespace Website.Models.CMS
{
    public class Mill : Tool
    {
        [Required]
        public int NoCuttingEddges { get; set; }
    }
}
