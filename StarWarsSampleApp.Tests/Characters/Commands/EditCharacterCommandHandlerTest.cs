using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Respawn;
using Shouldly;
using StarWarsSampleApp.Application.Characters.Commands.EditCharacter;
using StarWarsSampleApp.Application.Exceptions;
using StarWarsSampleApp.Tests.Builders;
using StarWarsSampleApp.Tests.Infrastructure;
using Xunit;

namespace StarWarsSampleApp.Tests.Characters.Commands
{
    [Collection("Test collection")]
    public class EditCharacterCommandHandlerTest : IAsyncLifetime
    {
        private readonly TestFixture _testFixture;
        private readonly Checkpoint _checkpoint;

        public EditCharacterCommandHandlerTest(TestFixture fixture)
        {
            _testFixture = fixture;
            _checkpoint = new Checkpoint();
        }

        public Task InitializeAsync() => _checkpoint.Reset(_testFixture.ConnectionString);

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Edit_character_command_handler_should_return_id_of_updated_character()
        {
            // Arrange
            var episode = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var characterToUpdate = new CharacterBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var command = new EditCharacterCommand
            {
                Id = characterToUpdate.Id,
                Name = "changed name",
                EpisodesIds = new[] {episode.Id},
                FriendsIds = new int[] {}
            };
            var commandHandler = new EditCharacterCommandHandler(_testFixture.Context, _testFixture.Mapper);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldBeOfType(typeof(int));
            result.ShouldBe(characterToUpdate.Id);
        }

        [Fact]
        public async Task Edit_character_command_handler_should_throw_not_found_exception()
        {
            // Arrange
            var episode = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var characterToUpdate = new CharacterBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var command = new EditCharacterCommand
            {
                Id = characterToUpdate.Id + 1,
                Name = "name",
                EpisodesIds = new[] { episode.Id },
                FriendsIds = new int[] { }
            };
            var commandHandler = new EditCharacterCommandHandler(_testFixture.Context, _testFixture.Mapper);

            async Task Act() => await commandHandler.Handle(command, CancellationToken.None);
            var ex = await Record.ExceptionAsync(Act);

            // Assert
            ex.ShouldBeOfType<NotFoundException>();
        }
    }
}
