using System.ComponentModel.DataAnnotations;

namespace CutItUp.Data.Data.CMS.MainWebsite
{
    public class WhyUsSection
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public ICollection<string> Reasons { get; set; } = new List<string>();
    }
}
