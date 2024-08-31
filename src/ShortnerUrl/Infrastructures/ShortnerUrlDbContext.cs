using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using ShortnerUrl.Models;

namespace ShortnerUrl.Infrastructures
{
    public class ShortnerUrlDbContext:DbContext
    {
        public ShortnerUrlDbContext(DbContextOptions<ShortnerUrlDbContext> options) : base(options) 
        { 

        }

        public DbSet<UrlTag> urlTags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UrlTag>().ToCollection("UrlTags");
        }
    }
}
