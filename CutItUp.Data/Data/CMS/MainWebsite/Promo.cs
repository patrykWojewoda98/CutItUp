using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CutItUp.Data.Data.CMS.MainWebsite
{
    public class Promo
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Website")]
        public int WebsiteId { get; set; }
        public MainWebsite Website { get; set; } = null!;
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Url { get; set; }

    }
}
