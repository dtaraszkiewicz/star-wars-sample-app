using System.Collections.Generic;

namespace StarWarsSampleApp.Domain.Entities
{
    public class Episode
    {
        public Episode()
        {
            Characters = new HashSet<CharacterEpisode>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CharacterEpisode> Characters { get; set; }
    }
}
