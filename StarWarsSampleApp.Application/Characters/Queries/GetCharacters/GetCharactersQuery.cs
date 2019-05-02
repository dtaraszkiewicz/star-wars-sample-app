using System.Collections.Generic;
using MediatR;

namespace StarWarsSampleApp.Application.Characters.Queries.GetCharacters
{
    public class GetCharactersQuery : IRequest<IList<GetCharacterViewModel>>
    {
    }
}
