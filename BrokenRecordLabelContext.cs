using Microsoft.EntityFrameworkCore;
namespace RhythmsGonnaGetYou
{
    class BrokenRecordLabelContext : DbContext
    {
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("server=localhost;database=BrokenRecordLabel");
        }
    }
}