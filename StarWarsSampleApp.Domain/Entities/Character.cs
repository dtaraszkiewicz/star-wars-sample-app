using System.Collections.Generic;

namespace StarWarsSampleApp.Domain.Entities
{
    public class Character
    {
        public Character()
        {
            Friends = new HashSet<Friendship>();
            Episodes = new HashSet<CharacterEpisode>();
        }

        public int Id { get; set; }
        public int Name { get; set; }

        public ICollection<Friendship> Friends { get; set; }
        public ICollection<CharacterEpisode> Episodes { get; set; }
    }
}
