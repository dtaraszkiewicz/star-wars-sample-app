using MediatR;

namespace StarWarsSampleApp.Application.Episodes.Commands.DeleteEpisode
{
    public class DeleteEpisodeCommand : IRequest
    {
        public int Id { get; set; }
    }
}
