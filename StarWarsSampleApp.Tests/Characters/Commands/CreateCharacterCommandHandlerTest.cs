using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Respawn;
using Shouldly;
using StarWarsSampleApp.Application.Characters.Commands.CreateCharacter;
using StarWarsSampleApp.Tests.Builders;
using StarWarsSampleApp.Tests.Infrastructure;
using Xunit;

namespace StarWarsSampleApp.Tests.Characters.Commands
{
    [Collection("Test collection")]
    public class CreateCharacterCommandHandlerTest : IAsyncLifetime
    {
        private readonly TestFixture _testFixture;
        private readonly Checkpoint _checkpoint;

        public CreateCharacterCommandHandlerTest(TestFixture fixture)
        {
            _testFixture = fixture;
            _checkpoint = new Checkpoint();
        }

        public Task InitializeAsync() => _checkpoint.Reset(_testFixture.ConnectionString);

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Create_character_command_handler_should_return_id_of_created_character()
        {
            // Arrange
            var episode = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var commandHandler = new CreateCharacterCommandHandler(_testFixture.Context, _testFixture.Mapper);

            // Act
            var response = 
                await commandHandler.Handle(new CreateCharacterCommand
                    { Name = "test char", EpisodesIds = new [] {episode.Id}, FriendsIds = new int[]{}}, CancellationToken.None);

            // Assert
            response.ShouldBeOfType(typeof(int));
        }
    }
}
