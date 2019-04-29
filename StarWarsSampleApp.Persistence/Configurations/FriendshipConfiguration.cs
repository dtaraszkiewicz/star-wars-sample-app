using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarWarsSampleApp.Domain.Entities;

namespace StarWarsSampleApp.Persistence.Configurations
{
    class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasKey(x => new { x.CharacterId, x.FriendId });

            builder.HasOne(x => x.Character)
                .WithMany(y => y.Friends)
                .HasForeignKey(x => x.CharacterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
