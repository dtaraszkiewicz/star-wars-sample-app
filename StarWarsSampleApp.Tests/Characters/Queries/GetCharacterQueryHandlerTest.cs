using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Respawn;
using Shouldly;
using StarWarsSampleApp.Application.Characters.Queries;
using StarWarsSampleApp.Application.Exceptions;
using StarWarsSampleApp.Domain.Entities;
using StarWarsSampleApp.Tests.Builders;
using StarWarsSampleApp.Tests.Infrastructure;
using Xunit;

namespace StarWarsSampleApp.Tests.Characters.Queries
{
    [Collection("Test collection")]
    public class GetCharacterQueryHandlerTest : IAsyncLifetime
    {
        private readonly TestFixture _testFixture;
        private readonly Checkpoint _checkpoint;

        public GetCharacterQueryHandlerTest(TestFixture testFixture)
        {
            _testFixture = testFixture;
            _checkpoint = new Checkpoint();
        }

        public Task InitializeAsync() => _checkpoint.Reset(_testFixture.ConnectionString);

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Get_character_query_handler_should_pass()
        {
            // Arrange
            var character = new CharacterBuilder().Generate().Build().First();
            var episode = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            character.Episodes.Add(new CharacterEpisode{CharacterId = character.Id, EpisodeId = episode.Id});
            _testFixture.Context.Characters.Update(character);
            _testFixture.Context.SaveChanges();
            var query = new GetCharacterQuery { Id = character.Id };
            var queryHandler = new GetCharacterQueryHandler(_testFixture.Context, _testFixture.Mapper);

            // Act
            var response = await queryHandler.Handle(query, CancellationToken.None);

            // Assert
            response.Name.ShouldBe(character.Name);
            response.Episodes.Count.ShouldBe(1);
            response.Episodes.First().Name.ShouldBe(episode.Name);
        }

        [Fact]
        public async Task Get_character_query_handler_should_throw_not_found_exception()
        {
            // Arrange
            var character = new CharacterBuilder().Generate().Build().First();
            var query = new GetCharacterQuery { Id = character.Id + 1 };
            var queryHandler = new GetCharacterQueryHandler(_testFixture.Context, _testFixture.Mapper);

            // Act
            async Task Act() => await queryHandler.Handle(query, CancellationToken.None);
            var ex = await Record.ExceptionAsync(Act);

            // Assert
            ex.ShouldBeOfType<NotFoundException>();
        }
    }
}
