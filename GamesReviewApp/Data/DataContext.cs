using GamesReviewApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesReviewApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
            
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Producent> Producers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<GameTag> GameTags { get; set; }
        public DbSet<GameProducent> GameProducers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameTag>()
                    .HasKey(pc => new { pc.GameId, pc.TagId });
            modelBuilder.Entity<GameTag>()
                    .HasOne(p => p.Game)
                    .WithMany(pc => pc.GameTags)
                    .HasForeignKey(p => p.GameId);
            modelBuilder.Entity<GameTag>()
                    .HasOne(p => p.Tag)
                    .WithMany(pc => pc.GameTags)
                    .HasForeignKey(c => c.TagId);

            modelBuilder.Entity<GameProducent>()
                    .HasKey(po => new { po.GameId, po.ProducentId });
            modelBuilder.Entity<GameProducent>()
                    .HasOne(p => p.Game)
                    .WithMany(pc => pc.GameProducers)
                    .HasForeignKey(p => p.GameId);
            modelBuilder.Entity<GameProducent>()
                    .HasOne(p => p.Producent)
                    .WithMany(pc => pc.GameProducers)
                    .HasForeignKey(c => c.ProducentId);
        }
    }
}
