using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarWarsSampleApp.Domain.Entities;

namespace StarWarsSampleApp.Persistence.Configurations
{
    public class CharacterEpisodeConfiguration : IEntityTypeConfiguration<CharacterEpisode>
    {
        public void Configure(EntityTypeBuilder<CharacterEpisode> builder)
        {
            builder.HasKey(x => new { x.CharacterId, x.EpisodeId });

            builder.HasOne(x => x.Character)
                .WithMany(y => y.Episodes)
                .HasForeignKey(x => x.CharacterId);

            builder.HasOne(x => x.Episode)
                .WithMany(y => y.Characters)
                .HasForeignKey(x => x.EpisodeId);
        }
    }
}
