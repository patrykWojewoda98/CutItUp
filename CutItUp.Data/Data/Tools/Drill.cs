using CutItUp.Data.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutItUp.Data.Data.Tools
{
    public class Drill : Tool
    {
        [Required]
        public float Angle { get; set; }
    }
}
