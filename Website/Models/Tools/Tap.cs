using System.ComponentModel.DataAnnotations;
using Website.Models.Abstractions;

namespace Website.Models.CMS
{
    public class Tap: Tool
    {
        [Required]
        public bool PassThrough { get; set; }

    }
}
