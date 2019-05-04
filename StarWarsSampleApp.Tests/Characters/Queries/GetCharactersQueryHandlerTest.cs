using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Respawn;
using Shouldly;
using StarWarsSampleApp.Application.Characters.Queries.GetCharacter;
using StarWarsSampleApp.Application.Characters.Queries.GetCharacters;
using StarWarsSampleApp.Tests.Builders;
using StarWarsSampleApp.Tests.Infrastructure;
using Xunit;

namespace StarWarsSampleApp.Tests.Characters.Queries
{
    [Collection("Test collection")]
    public class GetCharactersQueryHandlerTest : IAsyncLifetime
    {
        private readonly TestFixture _testFixture;
        private readonly Checkpoint _checkpoint;

        public GetCharactersQueryHandlerTest(TestFixture fixture)
        {
            _testFixture = fixture;
            _checkpoint = new Checkpoint();
        }

        public Task InitializeAsync() => _checkpoint.Reset(_testFixture.ConnectionString);

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Get_characters_query_handler_should_return_list_of_episodes()
        {
            // Arrange
            var characters = new CharacterBuilder().Generate(10).SaveChanges(_testFixture.Context).Build();
            var queryHandler = new GetCharactersQueryHandler(_testFixture.Context, _testFixture.Mapper);

            // Act
            var response = await queryHandler.Handle(new GetCharactersQuery(), CancellationToken.None);

            // Assert
            response.ShouldBeOfType(typeof(List<GetCharacterViewModel>));
            response.Count.ShouldBe(characters.Count);
        }

        [Fact]
        public async Task Get_characters_query_handler_should_return_empty_list()
        {
            // Arrange
            var queryHandler = new GetCharactersQueryHandler(_testFixture.Context, _testFixture.Mapper);

            // Act
            var response = await queryHandler.Handle(new GetCharactersQuery(), CancellationToken.None);

            // Assert
            response.ShouldBeOfType(typeof(List<GetCharacterViewModel>));
            response.ShouldBeEmpty();
        }

        [Fact]
        public async Task Get_characters_query_handler_should_return_ten_episodes()
        {
            // Arrange
            new CharacterBuilder().Generate(15).SaveChanges(_testFixture.Context).Build();
            var queryHandler = new GetCharactersQueryHandler(_testFixture.Context, _testFixture.Mapper);

            // Act
            var response = await queryHandler.Handle(new GetCharactersQuery { PageNumber = 1, PageSize = null }, CancellationToken.None);

            // Assert
            response.ShouldBeOfType(typeof(List<GetCharacterViewModel>));
            response.Count.ShouldBe(10);
        }

        [Fact]
        public async Task Get_episodes_query_handler_should_return_last_page()
        {
            // Arrange
            var lastCharacter = new CharacterBuilder().Generate(20).SaveChanges(_testFixture.Context).Build().Last();
            var queryHandler = new GetCharactersQueryHandler(_testFixture.Context, _testFixture.Mapper);

            // Act
            var response = await queryHandler.Handle(new GetCharactersQuery { PageNumber = 17, PageSize = 4 }, CancellationToken.None);

            // Assert
            response.ShouldBeOfType(typeof(List<GetCharacterViewModel>));
            response.Last().Name.ShouldBe(lastCharacter.Name);
        }
    }
}
