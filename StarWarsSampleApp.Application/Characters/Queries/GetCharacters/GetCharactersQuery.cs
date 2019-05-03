using System.Collections.Generic;
using MediatR;
using StarWarsSampleApp.Application.Characters.Queries.GetCharacter;

namespace StarWarsSampleApp.Application.Characters.Queries.GetCharacters
{
    public class GetCharactersQuery : IRequest<IList<GetCharacterViewModel>>
    {
    }
}
