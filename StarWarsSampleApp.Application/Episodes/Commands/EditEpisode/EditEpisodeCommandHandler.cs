using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StarWarsSampleApp.Persistence;

namespace StarWarsSampleApp.Application.Episodes.Commands.EditEpisode
{
    public class EditEpisodeCommandHandler : IRequestHandler<EditEpisodeCommand, int>
    {
        private readonly StarWarsSampleAppDbContext _context;

        public EditEpisodeCommandHandler(StarWarsSampleAppDbContext context)
        {
            _context = context;
        }

        public Task<int> Handle(EditEpisodeCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
