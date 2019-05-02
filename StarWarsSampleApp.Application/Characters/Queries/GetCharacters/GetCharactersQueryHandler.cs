using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StarWarsSampleApp.Persistence;

namespace StarWarsSampleApp.Application.Characters.Queries.GetCharacters
{
    public class GetCharactersQueryHandler : IRequestHandler<GetCharactersQuery, IList<GetCharacterViewModel>>
    {
        private readonly StarWarsSampleAppDbContext _context;
        private readonly IMapper _mapper;

        public GetCharactersQueryHandler(StarWarsSampleAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<GetCharacterViewModel>> Handle(GetCharactersQuery request, CancellationToken cancellationToken)
        {
            var characters =
                _mapper.Map<IList<GetCharacterViewModel>>(await _context.Characters
                    .Include(x => x.Episodes)
                    .ThenInclude(e => e.Episode)
                    .Include(x => x.Friends)
                    .ThenInclude(f => f.Friend).ToListAsync(cancellationToken));

            return characters;
        }
    }
}
