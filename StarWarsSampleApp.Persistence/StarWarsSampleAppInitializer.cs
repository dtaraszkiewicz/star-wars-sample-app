using StarWarsSampleApp.Domain.Entities;
using System.Linq;

namespace StarWarsSampleApp.Persistence
{
    public class StarWarsSampleAppInitializer
    {
        public static void Initialize(StarWarsSampleAppDbContext context)
        {
            var initializer = new StarWarsSampleAppInitializer();
            initializer.SeedDb(context);
        }

        public void SeedDb(StarWarsSampleAppDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Episodes.Any())
            {
                SeedEpisodes(context);
            }

            if (!context.Characters.Any())
            {
                SeedCharacters(context);
            }

            if (!context.CharacterEpisodes.Any())
            {
                SeedCharacterEpisodes(context);
            }

            if (!context.Friendships.Any())
            {
                MakeFriend(context);
            }
        }

        private void SeedEpisodes(StarWarsSampleAppDbContext context)
        {
            context.Episodes.AddRange(new Episode[]
            {
                new Episode() { Name = "A new Hope" },
                new Episode() { Name = "The Empire Strikes Back" },
                new Episode() { Name = "Return of the Jedi" },
                new Episode() { Name = "The Phantom Menace" },
                new Episode() { Name = "Attack of the Clones" },
                new Episode() { Name = "Revenge of the Sith" },
                new Episode() { Name = "The Force Awakens" },
                new Episode() { Name = "The Last Jedi" },
                new Episode() { Name = "The Rise of Skywalker" }
            });

            context.SaveChanges();
        }

        private void SeedCharacters(StarWarsSampleAppDbContext context)
        {
            var episode4 = context.Episodes.Where(x => x.Name == "A new Hope").SingleOrDefault();

            context.Characters.AddRange(new Character[]
            {
                new Character() { Name = "Luke Skywalker" },
                new Character() { Name = "Han Solo" }
            });

            context.SaveChanges();
        }

        private void SeedCharacterEpisodes(StarWarsSampleAppDbContext context)
        {
            var episode4 = context.Episodes.Where(x => x.Name == "A new Hope").SingleOrDefault();
            var luke = context.Characters.Where(x => x.Name == "Luke Skywalker").SingleOrDefault();
            var han = context.Characters.Where(x => x.Name == "Han Solo").SingleOrDefault();

            context.CharacterEpisodes.AddRange(new CharacterEpisode[]
            {
                new CharacterEpisode() { CharacterId = luke.Id, EpisodeId = episode4.Id},
                new CharacterEpisode() { CharacterId = han.Id, EpisodeId = episode4.Id}
            });

            context.SaveChanges();
        }

        private void MakeFriend(StarWarsSampleAppDbContext context)
        {

            var luke = context.Characters.Where(x => x.Name == "Luke Skywalker").SingleOrDefault();
            var han = context.Characters.Where(x => x.Name == "Han Solo").SingleOrDefault();

            context.Friendships.Add(new Friendship() { CharacterId = luke.Id, FriendId = han.Id });

            context.SaveChanges();
        }
    }
}
