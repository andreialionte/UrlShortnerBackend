using Microsoft.EntityFrameworkCore;

namespace UrlShortner.Data
{
    public class DataContext : DbContext
    {
        public DbSet<UrlMapping> urlMappings { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*            base.OnModelCreating(modelBuilder);*/
            modelBuilder.HasDefaultSchema("UrlShortnerSchema");

            modelBuilder.Entity<UrlMapping>().ToTable("UrlMapping").HasKey(k => k.Id);
        }
    }
}
