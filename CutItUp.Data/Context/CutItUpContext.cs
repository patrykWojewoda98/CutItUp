using CutItUp.Data.Data;
using CutItUp.Data.Data.Abstractions;
using CutItUp.Data.Data.CMS;
using CutItUp.Data.Data.CMS.GPT;
using CutItUp.Data.Data.CMS.MainWebsite;
using CutItUp.Data.Data.Tools;
using CutItUp.Data.Data.User;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<Promo> Promo { get; set; } = default!;
        public DbSet<WhyUsSection> WhyUsSection { get; set; } = default!;
        public DbSet<Cart> Cart { get; set; } = default!;
        public DbSet<CartTool> CartTool { get; set; } = default!;
        public DbSet<GPTMessage> GPTMessage { get; set; } = default!;
        public DbSet<TitleDescriptionPart> TitleDescriptionPart { get; set; } = default!;
        public DbSet<ListPart> ListPart { get; set; } = default!;
        public DbSet<Website> Website { get; set; } = default!;
        public DbSet<Tool> Tool { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;
    }
}