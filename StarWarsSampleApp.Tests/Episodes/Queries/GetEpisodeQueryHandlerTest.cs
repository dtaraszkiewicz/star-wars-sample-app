using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Respawn;
using Shouldly;
using StarWarsSampleApp.Application.Episodes.Queries.GetEpisode;
using StarWarsSampleApp.Application.Exceptions;
using StarWarsSampleApp.Tests.Builders;
using StarWarsSampleApp.Tests.Infrastructure;
using Xunit;

namespace StarWarsSampleApp.Tests.Episodes.Queries
{
    [Collection("Test collection")]
    public class GetEpisodeQueryHandlerTest : IAsyncLifetime
    {
        private readonly TestFixture _testFixture;
        private readonly Checkpoint _checkpoint;

        public GetEpisodeQueryHandlerTest(TestFixture fixture)
        {
            _testFixture = fixture;
            _checkpoint = new Checkpoint();
        }

        public Task InitializeAsync() => _checkpoint.Reset(_testFixture.ConnectionString);

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Get_episode_query_handler_should_pass()
        {
            // Arrange
            var episode = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var query = new GetEpisodeQuery {Id = episode.Id};
            var queryHandler = new GetEpisodeQueryHandler(_testFixture.Context, _testFixture.Mapper);

            // Act
            var response = await queryHandler.Handle(query, CancellationToken.None);

            // Assert
            response.Name.ShouldBe(episode.Name);
            response.ShouldBeOfType(typeof(EpisodeViewModel));
        }

        [Fact]
        public async Task Get_episode_query_handler_should_throw_not_found_exception()
        {
            // Arrange
            var episode = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var query = new GetEpisodeQuery { Id = episode.Id + 1 };
            var queryHandler = new GetEpisodeQueryHandler(_testFixture.Context, _testFixture.Mapper);

            // Act
            async Task Act() => await queryHandler.Handle(query, CancellationToken.None);
            var ex = await Record.ExceptionAsync(Act);

            // Assert
            ex.ShouldBeOfType<NotFoundException>();
        }
    }
}
