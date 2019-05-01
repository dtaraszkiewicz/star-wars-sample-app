using StarWarsSampleApp.Tests.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Respawn;
using Shouldly;
using StarWarsSampleApp.Domain.Entities;
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
        public async Task firstTest()
        {
            _testFixture.Context.Episodes.Add(new Episode {Name = "test"});
            await _testFixture.Context.SaveChangesAsync();

            var episode = _testFixture.Context.Episodes.FirstOrDefault();

            episode.Name.ShouldBe("test");
        }
    }
}
