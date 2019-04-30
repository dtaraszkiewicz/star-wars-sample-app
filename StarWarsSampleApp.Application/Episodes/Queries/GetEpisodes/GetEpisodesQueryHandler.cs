using MediatR;
using Microsoft.EntityFrameworkCore;
using StarWarsSampleApp.Application.Episodes.Queries.GetEpisode;
using StarWarsSampleApp.Persistence;
using System.Collections.Generic;
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
            //var episodes = await _context.Episodes.Select(x => new EpisodeViewModel
            //{
            //    Name = x.Name
            //}).ToListAsync();

            var episodes = _mapper.Map<IList<EpisodeViewModel>>(await _context.Episodes
                .ToListAsync(cancellationToken));

            return episodes;
        }
    }
}
