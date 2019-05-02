using System.Collections.Generic;
using Bogus;
using StarWarsSampleApp.Domain.Entities;
using StarWarsSampleApp.Persistence;

namespace StarWarsSampleApp.Tests.Builders
{
    public class CharacterBuilder
    {
        private readonly Faker<Character> _characterFaker;
        private List<Character> _characters;

        public CharacterBuilder()
        {
            _characterFaker = new Faker<Character>()
                .StrictMode(false)
                .CustomInstantiator(e => new Character())
                .RuleFor(x => x.Name, s => s.Name.FullName());
        }

        public CharacterBuilder Generate(int amount = 1)
        {
            _characters = _characterFaker.Generate(amount);
            return this;
        }

        public CharacterBuilder SaveChanges(StarWarsSampleAppDbContext context)
        {
            context.Characters.AddRange(_characters);
            context.SaveChanges();
            return this;
        }

        public List<Character> Build()
        {
            return _characters;
        }
    }
}
