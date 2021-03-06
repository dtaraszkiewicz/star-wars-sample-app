﻿using System.Linq;
using System.Threading.Tasks;
using Respawn;
using Shouldly;
using StarWarsSampleApp.Application.Episodes.Commands.EditEpisode;
using StarWarsSampleApp.Tests.Builders;
using StarWarsSampleApp.Tests.Infrastructure;
using Xunit;

namespace StarWarsSampleApp.Tests.Episodes.Validators
{
    [Collection("Test collection")]
    public class EditEpisodeCommandValidatorTest : IAsyncLifetime
    {
        private readonly TestFixture _testFixture;
        private readonly Checkpoint _checkpoint;

        public EditEpisodeCommandValidatorTest(TestFixture fixture)
        {
            _testFixture = fixture;
            _checkpoint = new Checkpoint();
        }

        public Task InitializeAsync() => _checkpoint.Reset(_testFixture.ConnectionString);

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Edit_episode_command_validator_should_pass()
        {
            // Arrange
            var episodeInDb = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var validator = new EditEpisodeCommandValidator(_testFixture.Context);
            var command = new EditEpisodeCommand { Id = episodeInDb.Id, Name = episodeInDb.Name + "something" };

            // Act
            var validationResult = await validator.ValidateAsync(command);

            // Assert
            validationResult.Errors.ShouldBeEmpty();
        }

        [Fact]
        public async Task Edit_episode_command_validator_should_return_empty_name_validation_error()
        {
            // Arrange
            var episodeInDb = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var validator = new EditEpisodeCommandValidator(_testFixture.Context);
            var command = new EditEpisodeCommand { Id = episodeInDb.Id, Name = "" };

            // Act
            var validationResult = await validator.ValidateAsync(command);

            // Assert
            validationResult.Errors.Count.ShouldBe(1);
            validationResult.Errors.First().ErrorCode.ShouldBe("NotEmptyValidator");
        }

        [Fact]
        public async Task Edit_episode_command_validator_should_return_unique_name_validation_error()
        {
            // Arrange
            var episodesInDb = new EpisodeBuilder().Generate(2).SaveChanges(_testFixture.Context).Build();
            var validator = new EditEpisodeCommandValidator(_testFixture.Context);
            var command = new EditEpisodeCommand { Id = episodesInDb.First().Id, Name = episodesInDb.Last().Name };

            // Act
            var validationResult = await validator.ValidateAsync(command);

            // Assert
            validationResult.Errors.Count.ShouldBe(1);
            validationResult.Errors.First().ErrorCode.ShouldBe("PredicateValidator");
            validationResult.Errors.First().ErrorMessage.ShouldBe("The episode name has to be unique");
        }

        [Fact]
        public async Task Edit_episode_command_validator_should_return_is_deleted_validation_error()
        {
            // Arrange
            var episodeInDb = new EpisodeBuilder().Generate().SaveChanges(_testFixture.Context).Build().First();
            var validator = new EditEpisodeCommandValidator(_testFixture.Context);
            var entity = _testFixture.Context.Episodes.Find(episodeInDb.Id);
            entity.IsActive = false;
            _testFixture.Context.Episodes.Update(entity);
            _testFixture.Context.SaveChanges();
            var command = new EditEpisodeCommand { Id = episodeInDb.Id, Name = episodeInDb.Name };

            // Act
            var validationResult = await validator.ValidateAsync(command);

            // Assert
            validationResult.Errors.Count.ShouldBe(1);
            validationResult.Errors.First().ErrorCode.ShouldBe("PredicateValidator");
            validationResult.Errors.First().ErrorMessage.ShouldBe("This episode was deleted");
        }
    }
}
