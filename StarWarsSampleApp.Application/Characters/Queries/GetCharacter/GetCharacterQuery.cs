using MediatR;

namespace StarWarsSampleApp.Application.Characters.Queries
{
    public class GetCharacterQuery : IRequest<GetCharacterViewModel>
    {
        public int Id { get; set; }
    }
}
