using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StarWarsSampleApp.Application.Characters.Queries.GetCharacter;
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
            //TODO think of better way of pagination
            if (request.PageNumber.HasValue)
            {
                request.PageSize = request.PageSize ?? 10;

                var query = _context.Characters;
                var count = await query.Where(x => x.IsActive.Value).CountAsync(cancellationToken);
                var totalPages = (int)Math.Ceiling(count / (float)request.PageSize);

                request.PageNumber = request.PageNumber <= totalPages ? request.PageNumber : totalPages;

                var characters =
                    _mapper.Map<IList<GetCharacterViewModel>>(await _context.Characters
                        .Where(x => x.IsActive.Value)
                        .Include(x => x.Episodes)
                        .ThenInclude(e => e.Episode)
                        .Include(x => x.Friends)
                        .ThenInclude(f => f.Friend)
                        .Skip((request.PageNumber.Value - 1) * request.PageSize.Value)
                        .Take(request.PageSize.Value)
                        .ToListAsync(cancellationToken));

                return characters;
            }
            else
            {
                var characters =
                    _mapper.Map<IList<GetCharacterViewModel>>(await _context.Characters
                        .Where(x => x.IsActive.Value)
                        .Include(x => x.Episodes)
                        .ThenInclude(e => e.Episode)
                        .Include(x => x.Friends)
                        .ThenInclude(f => f.Friend).ToListAsync(cancellationToken));

                return characters;
            }

        }
    }
}
