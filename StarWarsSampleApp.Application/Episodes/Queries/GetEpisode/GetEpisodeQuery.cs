using MediatR;

namespace StarWarsSampleApp.Application.Episodes.Queries.GetEpisode
{
    public class GetEpisodeQuery : IRequest<EpisodeViewModel>
    {
        public int Id { get; set; }  
    }
}
