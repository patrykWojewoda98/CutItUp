using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutItUp.Data.Data.CMS
{
    public class Website
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public ICollection<TitleDescriptionPart> TitleDecriptionParts { get; set; } = new List<TitleDescriptionPart>();
        public ICollection<ListPart> ListParts { get; set; } = new List<ListPart>();

    }
}
