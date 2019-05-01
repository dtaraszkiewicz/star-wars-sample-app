using System.Threading;
using System.Threading.Tasks;
using MediatR;
using StarWarsSampleApp.Application.Exceptions;
using StarWarsSampleApp.Domain.Entities;
using StarWarsSampleApp.Persistence;

namespace StarWarsSampleApp.Application.Episodes.Commands.DeleteEpisode
{
    public class DeleteEpisodeCommandHandler : IRequestHandler<DeleteEpisodeCommand,Unit>
    {
        private readonly StarWarsSampleAppDbContext _context;

        public DeleteEpisodeCommandHandler(StarWarsSampleAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteEpisodeCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Episodes.Find(request.Id)
                         ?? throw new NotFoundException(typeof(Episode), request.Id);

            entity.IsActive = false;

            _context.Episodes.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
