using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Website.Models.CMS;

namespace Website.Data
{
    public class WebsiteContext : DbContext
    {
        public WebsiteContext (DbContextOptions<WebsiteContext> options)
            : base(options)
        {
        }

        public DbSet<Website.Models.CMS.Mill> Mill { get; set; } = default!;
        public DbSet<Website.Models.CMS.Drill> Drill { get; set; } = default!;
        public DbSet<Website.Models.CMS.SpecialTools> SpecialTools { get; set; } = default!;
        public DbSet<Website.Models.CMS.Tap> Tap { get; set; } = default!;
    }
}
