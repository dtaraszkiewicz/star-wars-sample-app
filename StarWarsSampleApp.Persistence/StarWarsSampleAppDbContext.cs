using Microsoft.EntityFrameworkCore;
using StarWarsSampleApp.Domain.Entities;

namespace StarWarsSampleApp.Persistence
{
    public class StarWarsSampleAppDbContext : DbContext
    {
        public StarWarsSampleAppDbContext(DbContextOptions<StarWarsSampleAppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<CharacterEpisode> CharacterEpisodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StarWarsSampleAppDbContext).Assembly);
        }
    }
}
