using MediatR;

namespace StarWarsSampleApp.Application.Characters.Queries.GetCharacter
{
    public class GetCharacterQuery : IRequest<GetCharacterViewModel>
    {
        public int Id { get; set; }
    }
}
