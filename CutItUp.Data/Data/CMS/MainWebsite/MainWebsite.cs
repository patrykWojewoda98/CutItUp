using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutItUp.Data.Data.CMS.MainWebsite
{
    public class MainWebsite
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public ICollection<Promo> Promos { get; set; } = new List<Promo>();

        public WhyUsSection WhyUsSection { get; set; }
    }
}
