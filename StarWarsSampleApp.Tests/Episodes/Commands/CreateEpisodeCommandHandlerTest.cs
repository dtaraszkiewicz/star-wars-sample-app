using System.Threading;
using System.Threading.Tasks;
using Respawn;
using Shouldly;
using StarWarsSampleApp.Application.Episodes.Commands.CreateEpisode;
using StarWarsSampleApp.Tests.Infrastructure;
using Xunit;

namespace StarWarsSampleApp.Tests.Episodes.Commands
{
    [Collection("Test collection")]
    public class CreateEpisodeCommandHandlerTest : IAsyncLifetime
    {
        private readonly TestFixture _testFixture;
        private readonly Checkpoint _checkpoint;

        public CreateEpisodeCommandHandlerTest(TestFixture testFixture)
        {
            _testFixture = testFixture;
            _checkpoint = new Checkpoint();
        }

        public Task InitializeAsync() => _checkpoint.Reset(_testFixture.ConnectionString);

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Create_episode_command_handler_should_return_id_of_created_episode()
        {
            // Arrange
            var commandHandler = new CreateEpisodeCommandHandler(_testFixture.Context);

            // Act
            var response = await commandHandler.Handle(new CreateEpisodeCommand {Name = "test episode"},
                CancellationToken.None);

            // Assert
            response.ShouldBeOfType(typeof(int));
        }
    }
}
