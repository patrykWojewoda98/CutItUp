using CutItUp.Data.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutItUp.Data.Data
{
    public class CartTool
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CartId { get; set; }
        [Required]
        public int ToolId { get; set; }
        [ForeignKey("CartId")]
        public Cart Cart { get; set; } = null!;
        [ForeignKey("ToolId")]
        public Tool Tool { get; set; } = null!;
    }
}
