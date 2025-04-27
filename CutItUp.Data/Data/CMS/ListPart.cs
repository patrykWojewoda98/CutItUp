using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace CutItUp.Data.Data.CMS
{
    public class ListPart
    {
        [Key]
        public int Id { get; set; }
        
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [NotMapped]
        public List<string> ListContent { get; set; } = new();

        public string ListContentSerialized
        {
            get => JsonSerializer.Serialize(ListContent);
            set => ListContent = string.IsNullOrEmpty(value)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(value);
        }
        public string ImageUrl { get; set; } = string.Empty;

        [ForeignKey("Website")]
        public int WebsiteId { get; set; }

        [BindNever]
        public Website? Website { get; set; }
    }
}
