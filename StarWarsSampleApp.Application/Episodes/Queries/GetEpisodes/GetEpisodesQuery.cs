using MediatR;
using StarWarsSampleApp.Application.Episodes.Queries.GetEpisode;
using System.Collections.Generic;

namespace StarWarsSampleApp.Application.Episodes.Queries.GetEpisodes
{
    public class GetEpisodesQuery : IRequest<ICollection<EpisodeViewModel>>
    {

    }
}
