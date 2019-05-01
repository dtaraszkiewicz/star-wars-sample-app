using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Respawn;
using Shouldly;
using StarWarsSampleApp.Application.Episodes.Commands.EditEpisode;
using StarWarsSampleApp.Tests.Builders;
using StarWarsSampleApp.Tests.Infrastructure;
using Xunit;

namespace StarWarsSampleApp.Tests.Episodes.Commands
{
    public class EditEpisodeCommandHandlerTest : IAsyncLifetime
    {
        private readonly TestFixture _testFixture;
        private readonly Checkpoint _checkpoint;

        public EditEpisodeCommandHandlerTest(TestFixture fixture)
        {
            _testFixture = fixture;
            _checkpoint = new Checkpoint();
        }

        public Task InitializeAsync() => _checkpoint.Reset(_testFixture.ConnectionString);

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Edit_episode_command_handler_should_pass()
        {
            // Arrange
            var episode = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var command = new EditEpisodeCommand {Id = episode.Id, Name = "changed"};
            var commandHandler = new EditEpisodeCommandHandler(_testFixture.Context);

            // Act
            var response = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            response.ShouldBeOfType<int>();
            episode.Name.ShouldBe("changed");
        }
    }
}
