using System.Collections.Generic;
using StarWarsSampleApp.Tests.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using Respawn;
using Shouldly;
using StarWarsSampleApp.Application.Episodes.Queries.GetEpisode;
using StarWarsSampleApp.Application.Episodes.Queries.GetEpisodes;
using StarWarsSampleApp.Tests.Builders;
using Xunit;

namespace StarWarsSampleApp.Tests.Episodes.Queries
{
    [Collection("Test collection")]
    public class GetEpisodesQueryHandlerTest : IAsyncLifetime
    {
        private readonly TestFixture _testFixture;
        private readonly Checkpoint _checkpoint;

        public GetEpisodesQueryHandlerTest(TestFixture fixture)
        {
            _testFixture = fixture;
            _checkpoint = new Checkpoint();
        }

        public Task InitializeAsync() => _checkpoint.Reset(_testFixture.ConnectionString);

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Get_episodes_query_handler_should_return_list_of_episodes()
        {
            // Arrange
            var episodes = new EpisodeBuilder().Generate(10).SaveChanges(_testFixture.Context).Build();
            var queryHandler = new GetEpisodesQueryHandler(_testFixture.Context, _testFixture.Mapper);

            // Act
            var response = await queryHandler.Handle(new GetEpisodesQuery(), CancellationToken.None);

            // Assert
            response.ShouldBeOfType(typeof(List<EpisodeViewModel>));
            response.Count.ShouldBe(episodes.Count);
        }
    }
}
