using CutItUp.Data.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace CutItUp.Data.Data.Tools
{
    public class Mill : Tool
    {
        [Required]
        [Display(Name = "Libcza ostrzy")]
        public int NoCuttingEddges { get; set; }
    }
}
