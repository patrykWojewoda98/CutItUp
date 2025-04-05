using CutItUp.Data.Data;
using CutItUp.Data.Data.CMS.MainWebsite;
using CutItUp.Data.Data.Tools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutItUp.Data.Context
{
    public class CutItUpContext : DbContext
    {
        public CutItUpContext(DbContextOptions<CutItUpContext> options)
            : base(options)
        {
        }

        public DbSet<Mill> Mill { get; set; } = default!;
        public DbSet<Drill> Drill { get; set; } = default!;
        public DbSet<SpecialTool> SpecialTool { get; set; } = default!;
        public DbSet<Tap> Tap { get; set; } = default!;
        public DbSet<MainWebsite> MainWebsite { get; set; } = default!;
        public DbSet<Promo> Promo { get; set; } = default!;
        public DbSet<WhyUsSection> WhyUsSection { get; set; } = default!;
        public DbSet<Cart> Cart { get; set; } = default!;
        public DbSet<CartTool> CartTool { get; set; } = default!;
    }
}