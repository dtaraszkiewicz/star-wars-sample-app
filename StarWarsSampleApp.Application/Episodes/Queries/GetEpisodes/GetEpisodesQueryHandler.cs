using MediatR;
using Microsoft.EntityFrameworkCore;
using StarWarsSampleApp.Application.Episodes.Queries.GetEpisode;
using StarWarsSampleApp.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StarWarsSampleApp.Application.Episodes.Queries.GetEpisodes
{
    public class GetEpisodesQueryHandler : IRequestHandler<GetEpisodesQuery, ICollection<EpisodeViewModel>>
    {
        private readonly StarWarsSampleAppDbContext _context;

        public GetEpisodesQueryHandler(StarWarsSampleAppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<EpisodeViewModel>> Handle(GetEpisodesQuery request, CancellationToken cancellationToken)
        {
            var episodes = await _context.Episodes.Select(x => new EpisodeViewModel
            {
                Name = x.Name
            }).ToListAsync();

            return episodes;
        }
    }
}
