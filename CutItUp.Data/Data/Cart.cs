using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutItUp.Data.Data
{
    public class Cart
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public ICollection<CartTool> Tools { get; set; } = new List<CartTool>();

    }
}

