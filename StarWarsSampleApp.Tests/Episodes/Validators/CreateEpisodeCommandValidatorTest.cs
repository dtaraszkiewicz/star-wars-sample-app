using System.Linq;
using System.Threading.Tasks;
using Respawn;
using Shouldly;
using StarWarsSampleApp.Application.Episodes.Commands.CreateEpisode;
using StarWarsSampleApp.Tests.Builders;
using StarWarsSampleApp.Tests.Infrastructure;
using Xunit;

namespace StarWarsSampleApp.Tests.Episodes.Validators
{
    [Collection("Test collection")]
    public class CreateEpisodeCommandValidatorTest : IAsyncLifetime
    {
        private readonly TestFixture _testFixture;
        private readonly Checkpoint _checkpoint;

        public CreateEpisodeCommandValidatorTest(TestFixture testFixture)
        {
            _testFixture = testFixture;
            _checkpoint = new Checkpoint();
        }

        public Task InitializeAsync() => _checkpoint.Reset(_testFixture.ConnectionString);

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Create_episode_command_validator_should_pass()
        {
            // Arrange
            var episodeInDb = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var validator = new CreateEpisodeCommandValidator(_testFixture.Context);
            var command = new CreateEpisodeCommand { Name = episodeInDb.Name + "something"};

            // Act
            var validationResult = await validator.ValidateAsync(command);
        
            // Assert
            validationResult.Errors.ShouldBeEmpty();
        }

        [Fact]
        public async Task Create_episode_command_validator_should_return_empty_name_validation_error()
        {
            // Arrange
            var validator = new CreateEpisodeCommandValidator(_testFixture.Context);
            var command = new CreateEpisodeCommand { Name = "" };

            // Act
            var validationResult = await validator.ValidateAsync(command);

            // Assert
            validationResult.Errors.Count.ShouldBe(1);
            validationResult.Errors.First().ErrorCode.ShouldBe("NotEmptyValidator");
        }

        [Fact]
        public async Task Create_episode_command_validator_should_return_unique_name_validation_error()
        {
            // Arrange
            var episodeInDb = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var validator = new CreateEpisodeCommandValidator(_testFixture.Context);
            var command = new CreateEpisodeCommand { Name = episodeInDb.Name };

            // Act
            var validationResult = await validator.ValidateAsync(command);

            // Assert
            validationResult.Errors.Count.ShouldBe(1);
            validationResult.Errors.First().ErrorCode.ShouldBe("PredicateValidator");
            validationResult.Errors.First().ErrorMessage.ShouldBe("The episode name has to be unique");
        }
    }
}
