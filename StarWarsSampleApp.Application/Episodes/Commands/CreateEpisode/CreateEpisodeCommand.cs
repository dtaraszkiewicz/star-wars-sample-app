using MediatR;

namespace StarWarsSampleApp.Application.Episodes.Commands.CreateEpisode
{
    public class CreateEpisodeCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
