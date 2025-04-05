using CutItUp.Data.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutItUp.Data.Data.Tools
{
    public class SpecialTool : Tool
    {
        [Required]
        public int NoCuttingEddges { get; set; }

        public float? Angle { get; set; }
        public float? Radius { get; set; }
    }
}
