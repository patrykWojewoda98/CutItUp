using CutItUp.Data.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace CutItUp.Data.Data.Tools
{
    public class Tap : Tool
    {
        [Required]
        [Display(Name = "Czy przelotowy")]
        public bool PassThrough { get; set; }

    }
}
