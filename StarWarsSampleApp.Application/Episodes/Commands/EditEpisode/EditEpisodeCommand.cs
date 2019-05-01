using MediatR;
using StarWarsSampleApp.Application.Episodes.Commands.CreateEpisode;

namespace StarWarsSampleApp.Application.Episodes.Commands.EditEpisode
{
    public class EditEpisodeCommand : CreateEpisodeCommand, IRequest<int>
    {
        public int Id { get; set; }
    }
}
