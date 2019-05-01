using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Respawn;
using Shouldly;
using StarWarsSampleApp.Application.Episodes.Commands.DeleteEpisode;
using StarWarsSampleApp.Application.Exceptions;
using StarWarsSampleApp.Tests.Builders;
using StarWarsSampleApp.Tests.Infrastructure;
using Xunit;

namespace StarWarsSampleApp.Tests.Episodes.Commands
{
    [Collection("Test collection")]
    public class DeleteEpisodeCommandHandlerTest : IAsyncLifetime
    {
        private readonly TestFixture _testFixture;
        private readonly Checkpoint _checkpoint;

        public DeleteEpisodeCommandHandlerTest(TestFixture testFixture)
        {
            _testFixture = testFixture;
            _checkpoint = new Checkpoint();
        }

        public Task InitializeAsync() => _checkpoint.Reset(_testFixture.ConnectionString);

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Delete_episode_command_handler_should_pass()
        {
            // Arrange
            var episode = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var command = new DeleteEpisodeCommand {Id = episode.Id};
            var commandHandler = new DeleteEpisodeCommandHandler(_testFixture.Context);

            // Act
            var response = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            response.ShouldBe(Unit.Value);
            episode.IsActive.ShouldBe(false);
        }

        [Fact]
        public async Task Delete_episode_command_handler_should_throw_not_found_exception()
        {
            // Arrange
            var episode = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var command = new DeleteEpisodeCommand { Id = episode.Id + 1};
            var commandHandler = new DeleteEpisodeCommandHandler(_testFixture.Context);

            // Act
            async Task Act() => await commandHandler.Handle(command, CancellationToken.None);
            var ex = await Record.ExceptionAsync(Act);

            // Assert
            ex.ShouldBeOfType<NotFoundException>();
        }
    }
}
