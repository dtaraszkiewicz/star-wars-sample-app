using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Respawn;
using Shouldly;
using StarWarsSampleApp.Application.Characters.Commands.DeleteCharacter;
using StarWarsSampleApp.Application.Exceptions;
using StarWarsSampleApp.Tests.Builders;
using StarWarsSampleApp.Tests.Infrastructure;
using Xunit;

namespace StarWarsSampleApp.Tests.Characters.Commands
{
    [Collection("Test collection")]
    public class DeleteCharacterCommandHandlerTest : IAsyncLifetime
    {
        private readonly TestFixture _testFixture;
        private readonly Checkpoint _checkpoint;

        public DeleteCharacterCommandHandlerTest(TestFixture fixture)
        {
            _testFixture = fixture;
            _checkpoint = new Checkpoint();
        }

        public Task InitializeAsync() => _checkpoint.Reset(_testFixture.ConnectionString);

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Delete_character_command_handler_should_pass()
        {
            // Arrange
            var character = new CharacterBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var command = new DeleteCharacterCommand { Id = character.Id };
            var commandHandler = new DeleteCharacterCommandHandler(_testFixture.Context);

            // Act
            var response = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            response.ShouldBe(Unit.Value);
            character.IsActive.ShouldBe(false);
        }

        [Fact]
        public async Task Delete_character_command_handler_should_throw_not_found_exception()
        {
            // Arrange
            var character = new CharacterBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var command = new DeleteCharacterCommand { Id = character.Id + 1 };
            var commandHandler = new DeleteCharacterCommandHandler(_testFixture.Context);

            // Act
            async Task Act() => await commandHandler.Handle(command, CancellationToken.None);
            var ex = await Record.ExceptionAsync(Act);

            // Assert
            ex.ShouldBeOfType<NotFoundException>();
        }
    }
}
