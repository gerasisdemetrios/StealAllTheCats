using Microsoft.EntityFrameworkCore;
using StealAllTheCats.Models;

namespace StealAllTheCats
{
    public class StealAllTheCatsContext : DbContext
    {
        public StealAllTheCatsContext(DbContextOptions<StealAllTheCatsContext> options)
        : base(options)
        {
        }
        public DbSet<Cat> Cats { get; set; } = null!;

        public DbSet<Tag> Tags { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cat>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Cats);
        }
    }
}
