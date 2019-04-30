using StarWarsSampleApp.Application.Interfaces.Mapping;
using StarWarsSampleApp.Domain.Entities;

namespace StarWarsSampleApp.Application.Episodes.Queries.GetEpisode
{
    public class EpisodeViewModel : IMapFrom<Episode>
    {
        public string Name { get; set; }
    }
}
