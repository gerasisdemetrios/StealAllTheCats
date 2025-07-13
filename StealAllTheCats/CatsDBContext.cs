using Microsoft.EntityFrameworkCore;
using StealAllTheCats.Models;

namespace StealAllTheCats
{
    public class CatsDBContext : DbContext
    {
        public CatsDBContext(DbContextOptions<CatsDBContext> options)
        : base(options)
        {
        }
        public DbSet<CatEntity> Cats { get; set; } = null!;

        public DbSet<TagEntity> Tags { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CatEntity>()
                .HasMany(c => c.Tags)
                .WithMany(t => t.Cats)
                .UsingEntity(j => j.ToTable("CatTags"));
        }
    }
}
