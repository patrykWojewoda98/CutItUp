using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutItUp.Data.Data.CMS.MainWebsite
{
    public class WhyUsSection
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public ICollection<string> Reasons { get; set; } = new List<string>();
        public string ImageUrl { get; set; } = string.Empty;
        [ForeignKey("Website")]
        public int WebsiteId { get; set; }
        public MainWebsite Website { get; set; } = null!;
    }
}
