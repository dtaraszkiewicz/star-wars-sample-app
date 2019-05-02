using System.Linq;
using System.Threading.Tasks;
using Respawn;
using Shouldly;
using StarWarsSampleApp.Application.Characters.Commands.CreateCharacter;
using StarWarsSampleApp.Tests.Builders;
using StarWarsSampleApp.Tests.Infrastructure;
using Xunit;

namespace StarWarsSampleApp.Tests.Characters.Validators
{
    [Collection("Test collection")]
    public class CreateCharacterCommandValidatorTest : IAsyncLifetime
    {
        private readonly TestFixture _testFixture;
        private readonly Checkpoint _checkpoint;

        public CreateCharacterCommandValidatorTest(TestFixture fixture)
        {
            _testFixture = fixture;
            _checkpoint = new Checkpoint();
        }

        public Task InitializeAsync() => _checkpoint.Reset(_testFixture.ConnectionString);

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Create_character_command_validator_should_pass()
        {
            // Arrange
            var episode = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var command = new CreateCharacterCommand
                {EpisodesIds = new[] {episode.Id}, Name = "Han Solo", FriendsIds = new int[] { }};
            var validator = new CreateCharacterCommandValidator(_testFixture.Context);

            // Act
            var validationResult = await validator.ValidateAsync(command);

            // Assert
            validationResult.Errors.ShouldBeEmpty();
        }

        [Fact]
        public async Task Create_character_command_validator_should_return_no_episodes_validation_error()
        {
            // Arrange
            var episode = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var command = new CreateCharacterCommand
                { EpisodesIds = new[] { episode.Id + 1}, Name = "Han Solo", FriendsIds = new int[] { } };
            var validator = new CreateCharacterCommandValidator(_testFixture.Context);

            // Act
            var validationResult = await validator.ValidateAsync(command);

            // Assert
            validationResult.Errors.Count.ShouldBe(1);
            validationResult.Errors.First().ErrorCode.ShouldBe("PredicateValidator");
            validationResult.Errors.First().ErrorMessage.ShouldBe("Character has to star in at least one episode");
        }

        [Fact]
        public async Task Create_character_command_validator_should_return_unique_name_validation_error()
        {
            // Arrange
            var episode = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var character = new CharacterBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var command = new CreateCharacterCommand
                { EpisodesIds = new[] { episode.Id }, Name = character.Name, FriendsIds = new int[] { } };
            var validator = new CreateCharacterCommandValidator(_testFixture.Context);

            // Act
            var validationResult = await validator.ValidateAsync(command);

            // Assert
            validationResult.Errors.Count.ShouldBe(1);
            validationResult.Errors.First().ErrorCode.ShouldBe("PredicateValidator");
            validationResult.Errors.First().ErrorMessage.ShouldBe("The character name has to be unique");
        }
    }
}
