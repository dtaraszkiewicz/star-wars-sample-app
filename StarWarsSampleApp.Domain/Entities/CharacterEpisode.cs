namespace StarWarsSampleApp.Domain.Entities
{
    public class CharacterEpisode
    {
        public int CharacterId { get; set; }
        public int EpisodeId { get; set; }

        public virtual Character Character { get; set; }
        public virtual Episode Episode { get; set; }
    }
}
