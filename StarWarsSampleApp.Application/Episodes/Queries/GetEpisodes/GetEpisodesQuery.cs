using MediatR;
using StarWarsSampleApp.Application.Episodes.Queries.GetEpisode;
using System.Collections.Generic;

namespace StarWarsSampleApp.Application.Episodes.Queries.GetEpisodes
{
    public class GetEpisodesQuery : IRequest<IList<EpisodeViewModel>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
