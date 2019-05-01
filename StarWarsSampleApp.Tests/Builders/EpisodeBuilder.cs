using System.Collections.Generic;
using Bogus;
using StarWarsSampleApp.Domain.Entities;
using StarWarsSampleApp.Persistence;

namespace StarWarsSampleApp.Tests.Builders
{
    public class EpisodeBuilder
    {
        private readonly Faker<Episode> _episodeFaker;
        private List<Episode> _episodes;

        public EpisodeBuilder()
        {
            _episodeFaker = new Faker<Episode>()
                .StrictMode(false)
                .CustomInstantiator(e => new Episode())
                .RuleFor(x => x.Name, s => s.Company.CatchPhrase());
        }

        public EpisodeBuilder Generate(int amount = 1)
        {
            _episodes = _episodeFaker.Generate(amount);
            return this;

        }

        public EpisodeBuilder SaveChanges(StarWarsSampleAppDbContext context)
        {
            context.Episodes.AddRange(_episodes);
            context.SaveChanges();
            return this;
        }

        public List<Episode> Build()
        {
            return _episodes;
        }
    }
}
