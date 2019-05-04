using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StarWarsSampleApp.Application.Episodes.Queries.GetEpisode;
using StarWarsSampleApp.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace StarWarsSampleApp.Application.Episodes.Queries.GetEpisodes
{
    public class GetEpisodesQueryHandler : IRequestHandler<GetEpisodesQuery, IList<EpisodeViewModel>>
    {
        private readonly StarWarsSampleAppDbContext _context;
        private readonly IMapper _mapper;

        public GetEpisodesQueryHandler(StarWarsSampleAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<EpisodeViewModel>> Handle(GetEpisodesQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber.HasValue)
            {
                request.PageSize = request.PageSize ?? 10;

                var query = _context.Episodes;
                var count = await query.Where(x => x.IsActive.Value).CountAsync(cancellationToken);
                var totalPages = (int) Math.Ceiling(count / (float) request.PageSize);

                request.PageNumber = request.PageNumber <= totalPages ? request.PageNumber : totalPages;

                var episodes = _mapper.Map<IList<EpisodeViewModel>>(await _context.Episodes
                    .Where(x => x.IsActive.Value)
                    .Skip((request.PageNumber.Value - 1) * request.PageSize.Value)
                    .Take(request.PageSize.Value)
                    .ToListAsync(cancellationToken));

                return episodes;
            }
            else
            {
                var episodes = _mapper.Map<IList<EpisodeViewModel>>(await _context.Episodes
                    .Where(x => x.IsActive.Value)
                    .ToListAsync(cancellationToken));

                return episodes;
            }
        }
    }
}
