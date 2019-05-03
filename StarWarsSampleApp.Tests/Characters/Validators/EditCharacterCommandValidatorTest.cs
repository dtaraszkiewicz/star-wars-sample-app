using System.Linq;
using System.Threading.Tasks;
using Respawn;
using Shouldly;
using StarWarsSampleApp.Application.Characters.Commands.EditCharacter;
using StarWarsSampleApp.Tests.Builders;
using StarWarsSampleApp.Tests.Infrastructure;
using Xunit;

namespace StarWarsSampleApp.Tests.Characters.Validators
{
    [Collection("Test collection")]
    public class EditCharacterCommandValidatorTest : IAsyncLifetime
    {
        private readonly TestFixture _testFixture;
        private readonly Checkpoint _checkpoint;

        public EditCharacterCommandValidatorTest(TestFixture fixture)
        {
            _testFixture = fixture;
            _checkpoint = new Checkpoint();
        }

        public Task InitializeAsync() => _checkpoint.Reset(_testFixture.ConnectionString);

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Edit_character_command_validator_should_pass()
        {
            // Arrange
            var episode = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var characterToUpdate = new CharacterBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var command = new EditCharacterCommand
            {
                Id = characterToUpdate.Id,
                Name = characterToUpdate.Name,
                EpisodesIds = new[] {episode.Id},
                FriendsIds = new int[] { }
            };
            var validator = new EditCharacterCommandValidator(_testFixture.Context);

            // Act
            var validationResult = await validator.ValidateAsync(command);

            // Assert
            validationResult.Errors.ShouldBeEmpty();
        }

        [Fact]
        public async Task Edit_character_command_validator_should_return_no_episodes_validation_error()
        {
            // Arrange
            var episode = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var characterToUpdate = new CharacterBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var command = new EditCharacterCommand
            {
                Id = characterToUpdate.Id,
                Name = characterToUpdate.Name,
                EpisodesIds = new []{ episode.Id +1 },
                FriendsIds = new int[] {}
            };
            var validator = new EditCharacterCommandValidator(_testFixture.Context);

            // Act
            var validationResult = await validator.ValidateAsync(command);

            // Assert
            validationResult.Errors.Count.ShouldBe(1);
            validationResult.Errors.First().ErrorCode.ShouldBe("PredicateValidator");
            validationResult.Errors.First().ErrorMessage.ShouldBe("Character has to star in at least one episode");
        }

        [Fact]
        public async Task Edit_character_command_validator_should_return_unique_name_validation_error()
        {
            // Arrange
            var episode = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var characters = new CharacterBuilder().Generate(2).SaveChanges(_testFixture.Context).Build();
            var characterToUpdate = characters.First();
            var anotherCharacter = characters.Last();
            var command = new EditCharacterCommand
            {
                Id = characterToUpdate.Id,
                Name = anotherCharacter.Name,
                EpisodesIds = new[] { episode.Id },
                FriendsIds = new int[] { }
            };
            var validator = new EditCharacterCommandValidator(_testFixture.Context);

            // Act
            var validationResult = await validator.ValidateAsync(command);

            // Assert
            validationResult.Errors.Count.ShouldBe(1);
            validationResult.Errors.First().ErrorCode.ShouldBe("PredicateValidator");
            validationResult.Errors.First().ErrorMessage.ShouldBe("The character name has to be unique");
        }

        [Fact]
        public async Task Edit_character_command_validator_should_return_friend_with_itself_validation_error()
        {
            // Arrange
            var episode = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var characterToUpdate = new CharacterBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var command = new EditCharacterCommand
            {
                Id = characterToUpdate.Id,
                Name = characterToUpdate.Name,
                EpisodesIds = new[] { episode.Id },
                FriendsIds = new int[] { characterToUpdate.Id }
            };
            var validator = new EditCharacterCommandValidator(_testFixture.Context);

            // Act
            var validationResult = await validator.ValidateAsync(command);

            // Assert
            validationResult.Errors.Count.ShouldBe(1);
            validationResult.Errors.First().ErrorCode.ShouldBe("PredicateValidator");
            validationResult.Errors.First().ErrorMessage.ShouldBe("Character cannot be friend with itself");
        }
    }
}
